import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { formatDate } from '@angular/common';
import { BooleanSearchEntity, BooleanSearchEntryMetadata, BooleanSearchInput } from '../../shared/models/boolean-search';
import { dateTimeFormats, globalConstants } from '../../common/global-constants';

@Injectable({
	providedIn: 'root'
})

export class BooleanSearchService {

  constructor(private http: HttpClient) { }

  search<TModel>(query: BooleanSearchInput, booleanSearchEntity: BooleanSearchEntity, page: number): Observable<TModel> {

    let entityName = BooleanSearchEntity[booleanSearchEntity];
    let finalQuery = query.query.substring(query.query.indexOf(' ')).trim();

    return this.http.post<TModel>("Search/" + entityName + "/"+ page, { query: finalQuery });
  }

	getBooleanSearchDocumentation(): Map<string, BooleanSearchEntryMetadata[]> {

		let map = new Map<string, BooleanSearchEntryMetadata[]>();
    map.set('tables', this.getTables());

		map.set('musics', this.getMusics());
    map.set('playlists', this.getPlaylists());
    map.set('users', this.getUsersDocumentation());

		return map;
  }

  getTables(): BooleanSearchEntryMetadata[] {
    return [
      {
        name: 'musics',
        description: 'Filter the musics'
      },
      {
        name: 'playlists',
        description: 'Filter the playlists'
      },
      {
        name: 'users',
        description: 'Filter the users'
      },
    ];
  }

	mapRedirectUrl(query: string) {
		if(!query) {
			return;
		}

		let table = query.split(' ')[0];
		let redirectUrl = '';

		if(table == 'musics') {
			redirectUrl = '/musics';
		}
		else if(table == 'playlists') {
      redirectUrl = '/playlists';
		}
		else if(table == 'users') {
			redirectUrl = '/users';
		}

		return redirectUrl;
	}

  getMusics(): BooleanSearchEntryMetadata[] {
		return [
			{
				name: 'musics', 
				description: 'Filter the musics'
      },
      {
        name: 'playlists',
        description: 'Filter the playlists'
      },
			{
				name: 'users',
				description: 'Filter the users'
			},
			{
				name: 'billing',
				description: 'Filter the timeslips'
			},
		];
	}

	private getPlaylists(): BooleanSearchEntryMetadata[] {
		return [
		]
	}

	private getUsersDocumentation(): BooleanSearchEntryMetadata[] {
		
		let formatedDate = formatDate(new Date(), dateTimeFormats.date, globalConstants.languageCode);

		return [
			{
				name: 'firstName',
				description: 'For example, adding firstName:Jhon to the search will include all users which first name contains \'Jhon\''
			},
			{
				name: 'middleName',
				description: 'For example, adding middleName:Jhon to the search will include all users which middle name contains \'Jhon\''
			},
			{
				name: 'lastName',
				description: 'For example, adding lastName:Brian to the search will include all users which last name contains \'Brian\''
			},
			{
				name: 'initials',
				description: 'For example, adding initials:Jhon to the search will include all users which initials contains \'Jhon\''
			},
			{
				name: 'billingRate',
				description: 'For example, adding billingRate:3 to the search will include all users which billingRate is equal to 3'
			},
			{
				name: 'billingRate:lt',
				description: 'For example, adding billingRate:lt:3 to the search will include all users which billingRate is less than 3'
			},
			{
				name: 'billingRate:gt',
				description: 'For example, adding billingRate:gt:3 to the search will include all users which billingRate is graeter than 3'
			},
			{
				name: 'ssn',
				description: 'For example, adding ssn:123456 to the search will include all users which ssn contains \'123456\''
			},
			{
				name: 'terminationDate',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding terminationDate:${formatedDate} to the search will include all users which termination date is equal to the entered date`
			},
			{
				name: 'terminationDate:from',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding terminationDate:from:${formatedDate} to the search will include all users which termination date is greater than the entered date.`
			},
			{
				name: 'terminationDate:to',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding terminationDate:to:${formatedDate} to the search will include all users which termination date is less than the entered date.`
			},
			{
				name: 'barDate',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding barDate:${formatedDate} to the search will include all users which bar date is equal to the entered date`
			},
			{
				name: 'barDate:from',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding barDate:from:${formatedDate} to the search will include all users which bar date is greater than the entered date.`
			},
			{
				name: 'barDate:to',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding barDate:to:${formatedDate} to the search will include all users which bar date is less than the entered date.`
			},
			{
				name: 'hireDate',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding hireDate:${formatedDate} to the search will include all users which hire date is equal to the entered date`
			},
			{
				name: 'hireDate:from',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding hireDate:from:${formatedDate} to the search will include all users which hire date is greater than the entered date.`
			},
			{
				name: 'hireDate:to',
				description: `The date fromat should be the folowing: \'dd/MM/yyyy\'. 
											For example, adding hireDate:to:${formatedDate} to the search will include all users which hire date is less than the entered date.`
			},
			{
				name: 'jobTitle',
				description: 'For example, adding jobTitle:Hr to the search will include all users which job title contains \'Hr\''
			},
			{
				name: 'classification',
				description: 'For example, adding classification:judge to the search will include all users which classification contains \'judge\''
			}
		]
	}
}
