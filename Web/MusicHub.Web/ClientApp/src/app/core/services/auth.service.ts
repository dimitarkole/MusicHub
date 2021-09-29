import { EventEmitter, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { globalConstants } from 'src/app/common/global-constants';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import * as jwtDecode from "jwt-decode";
import UserInitials from '../../shared/models/userInitials';
import { UserFilter } from '../../shared/models/filterModels';
import UserInfo from '../../shared/models/userInfo';
import { Role } from '../../shared/models/role';
import { Permission } from '../../shared/models/permission';
import User from '../../shared/models/user';
import UserClassification from '../../shared/models/userClassification';
import UserFullName from '../../shared/models/userFullName';
import { ActiveDirectoryUser } from '../../shared/models/activeDirectoryUser';
import { PermissionType } from '../../shared/models/permissionType';
import LoginResponse from '../../shared/models/user';
import { UserPagination } from '../../users/models/userPagination';
import VerificationCode from '../../shared/models/verificationCode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
	public filteredUsers = new EventEmitter();
  private route = 'Users';
	private loginPath = 'Users/Login';
	private isLoggedInPath = 'Users/ValidateToken';
	private hasPermissionPath = 'Users/HasPermission';
	private createLocalUserPath = 'Users';
	private createAdUserPath = 'SuperAdmin/RegisterAd';
	private getUsersPath = 'Users';
	private getActiveDirectoryUsersPath = 'ActiveDirectory'
	private editUserPath = 'Users';
	private deletePath = 'Users/';
	private filterPath = 'Users/AdvancedSearch';
	private searchPath = 'SuperAdmin/FindUsers';
	private userToClone = null;
	private eventToClone = null;
	private phoneTypePath = 'PhoneTypes';
	private mailTypePath = 'EmailTypes';
	private addressTypePath = 'AddressTypes';
	private createContactPath = 'Contacts';
	private getMattersPath = 'Case/GetMatters';
	private editContactPath = 'Contacts';
	private getUsersClassificationsPath = 'Users/Classifications';
	private getFullNames = 'Users/GetFullNames';
  private rolesPath = 'Roles';
  private changeRolesPath = 'Users/ChangeRoles';
  private permissionsPath = 'Users/HasPermission';
  private getPermissionsPath = 'Permissions/GetOwnPermissions';
	private initialsPath = 'Users/GetInitials';
	private calendarEventPath = 'Events';
	private getItemTypesPath = 'Events/ItemTypes';
	private getStatusPath = 'Events/Statuses';
	private getEventTypePath = 'EventTypes';
	private getInitialsPath = 'Users/GetInitials';
	private userFiltersPath = 'Filters/Users';
  public isAuth = false;
  public isAdmin = false;
  public username: string;
  public role: string;
  isAuthChanged = new Subject<boolean>();

  constructor(private http: HttpClient, private router: Router) { }

  initializeAuthenticationState() {
    let token = this.getToken();
    if (!token) {
      this.isAuth = false;
      this.isAdmin = false;
      this.username = "";
    }
    else {
      try {
        var tokenData = jwtDecode(token);
        this.username = tokenData['unique_name'];
        this.isAuth = true;
        this.isAdmin = tokenData["role"] === "Administrator";
      }
      catch (Error) {
        this.isAuth = false;
        this.isAdmin = false;
        this.username = "";
      }
    }

    this.isAuthChanged.next(true);
  }

  getToken(): string {
    return localStorage.getItem(globalConstants.jwtTokenKey);
  }

  login(username: string, password: string) {
    return this.http.post(`${this.route}/Login`, { username, password });
  }

  getByEmail(user: User) {
    return this.http.post<User>(`${this.route}/GetByEmail`, user);
  }

  resendConfirmEmail(email: string) {
    return this.http.post(`${this.route}/resendConfirmEmail`, { email});
  }

  saveUserData(data: LoginResponse) {
		localStorage.setItem('token', data.token);
		localStorage.setItem('userId', data.userId);
		localStorage.setItem('username', data.username);
		localStorage.setItem('initials', data.initials);
		localStorage.setItem('roles', JSON.stringify(data.roles));
	}

	logout() {
		localStorage.removeItem('token');
		localStorage.removeItem('userId');
		localStorage.removeItem('username');
		localStorage.removeItem('initials');
		localStorage.removeItem('roles');
	}

	// getClassifications
	getInitials(): Observable<UserInitials[]> {
		return this.http.get<UserInitials[]>(this.initialsPath)
	}

	advancedFilter(page: number, filter: UserFilter): Observable<any> {
    return this.http.post(this.route + `/AdvancedSearch/${page}`, filter);
  }

  confirmEmail(userId: string, model: VerificationCode) {
    return this.http.put(this.route + `/ConfirmEmail/${userId}`, model);
  }

  all(page: number): Observable<UserPagination> {
    return this.http.get<UserPagination>(this.route + `/All/${page}`);
	}

	isInRole(role: string): boolean {
		let roles: string[] = JSON.parse(localStorage.getItem('roles'));
		if(!roles) {
			console.log('No roles into the storage');
			return false;
		}

		return roles.includes(role);
	}

	hasPermission(permissionType: PermissionType) : Observable<boolean> {
		return this.http.get<boolean>(`${this.hasPermissionPath}?permissionType=${permissionType}`);
	};

	getUsername(): string {
		return localStorage.getItem('username');
	}

	getCurrentUserInitials(): string {
		return localStorage.getItem('initials');
	}

  setToken(token: string) {
    return localStorage.setItem(globalConstants.jwtTokenKey, token);
  }

	isLoggedIn() : Observable<boolean> {
		return this.http.get<boolean>(`${this.isLoggedInPath}`);
	}

	getUserId(): string {
		return localStorage.getItem('userId');
	}

	getCalendarEvent(id) {
		return this.http.get(this.calendarEventPath + '/' + id);
	}

	create(data: any): Observable<any> {
		return this.http.post(this.createLocalUserPath, data);
	}

	searchNoFilter(data) {
		return this.http.post(this.searchPath, data);
	}

	createContact(data) {
		return this.http.post(this.createContactPath, data);
	}

	getRoles(): Observable<Array<Role>> {
		return this.http.get<Array<Role>>(this.rolesPath);
	}

	createRole(data): Observable<Role> {
		return this.http.post<Role>(this.rolesPath, data);
	}

	getPermissions(): Observable<Array<Permission>> {
    return this.http.get<Array<Permission>>(this.getPermissionsPath)
	}

	editContact(data, id) {
		return this.http.put(this.editContactPath + '/' + id, data);
	}

	edit(id: string, data: any): Observable<any> {
		return this.http.put(this.editUserPath + '/' + id, data);
  }

  changeRoles(id: string, data: any): Observable<any> {
    return this.http.put(this.changeRolesPath + '/' + id, data);
  }

	getUsers() {
		return this.http.get(this.getUsersPath);
	}

	 getActiveDirectoryUsers() : Observable<ActiveDirectoryUser[]>{
		return this.http.get<ActiveDirectoryUser[]>(this.getActiveDirectoryUsersPath);
	 }

	getUser(id: string): Observable<User> {
		return this.http.get<User>(this.getUsersPath + '/' + id);
	}

	getUserProfile(): Observable<User> {
		return this.http.get<User>(this.getUsersPath + '/MyProfile');
	}

	updateProfileImage(imageObj): Observable<any> {
		return this.http.post(this.getUsersPath + '/UploadProfileImage', imageObj);
	}

	deleteUser(id) {
		return this.http.delete(this.deletePath + id);
	}

	filterResult(filter) {
		return this.http.post(this.filterPath, filter);
	}

	filter(page:number, filters) {
		this.filteredUsers.emit(filters);
	}

	saveUserForClone(user) {
		this.userToClone = user;
	}

	getUserForClone() {
		if (!this.userToClone) {
			return null;
		}
		let obj = {...this.userToClone};
		this.userToClone = null;
		return obj;
	}

	saveEventForClone(event) {
		this.eventToClone = event;
	}

	getEventForClone() {
		if (!this.eventToClone) {
			return null;
		}
		let obj = {...this.eventToClone};
		this.eventToClone = null;
		return obj;
	}

	getMatters() {
		return this.http.get(this.getMattersPath);
	}
UserClassification
  getUserClassifications(): Observable<UserClassification[]> {
		return this.http.get<UserClassification[]>(this.getUsersClassificationsPath);
	}

	getUserFullNames(): Observable<UserFullName[]> {
		return this.http.get<UserFullName[]>(this.getFullNames);
	}

	getEventStatuses() {
		return this.http.get(this.getStatusPath);
	}

	getEventTypes() {
		return this.http.get(this.getEventTypePath);
	}

	createEvent(data) {
		return this.http.post(this.calendarEventPath, data);
	}

	sendUserFilter(data) {
		return this.http.post(this.userFiltersPath, data);
	}
}
