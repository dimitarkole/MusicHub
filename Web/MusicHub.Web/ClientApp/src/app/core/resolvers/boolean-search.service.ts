import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { formatDate } from '@angular/common';
import { BooleanSearchEntity, BooleanSearchEntryMetadata, BooleanSearchInput } from '../../shared/models/boolean-search';
import { dateTimeFormats, globalConstants } from '../../common/global-constants';
import { PermissionType } from '../../shared/models/permissionType';
import { setQueryString } from '../../common/QueryString';

@Injectable({
	providedIn: 'root'
})

export class BooleanSearchService {

  constructor(private http: HttpClient) { }

  search<TModel>(query: BooleanSearchInput, booleanSearchEntity: BooleanSearchEntity, page: number): Observable<TModel> {

    let entityName = BooleanSearchEntity[booleanSearchEntity];
    let finalQuery = query.query.substring(query.query.indexOf(' ')).trim();
    return this.http.get<TModel>("Search/" + entityName + "/" + page, { params: { query : finalQuery} });
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
        name: 'Musics',
        description: 'Filter the musics',
        permission: PermissionType.User,
      },
      {
        name: 'Playlists',
        description: 'Filter the playlists',
        permission: PermissionType.User,
      },
      {
        name: 'Members',
        description: 'Filter the users',
        permission: PermissionType.User,
      },
      {
        name: 'Categories',
        description: 'Filter the users',
        permission: PermissionType.CategoryAdministrator,
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
        description: 'Filter the musics',
        permission: PermissionType.User,
      },
      {
        name: 'playlists',
        description: 'Filter the playlists',
        permission: PermissionType.User,
      },
			{
				name: 'users',
        description: 'Filter the users',
        permission: PermissionType.User,
			},
			{
				name: 'billing',
        description: 'Filter the timeslips',
        permission: PermissionType.UserAdministration,
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
        description: 'For example, adding firstName:Jhon to the search will include all users which first name contains \'Jhon\'',
        permission: PermissionType.User,
			},
			{
				name: 'middleName',
        description: 'For example, adding middleName:Jhon to the search will include all users which middle name contains \'Jhon\'',
        permission: PermissionType.User,
			},
			{
				name: 'lastName',
        description: 'For example, adding lastName:Brian to the search will include all users which last name contains \'Brian\'',
        permission: PermissionType.User,
			},
		]
	}
}
