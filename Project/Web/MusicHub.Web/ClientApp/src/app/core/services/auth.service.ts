import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { globalConstants } from 'src/app/common/global-constants';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import * as jwtDecode from "jwt-decode";
import User from '../../components/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private route: string = 'Identity';
  private isLoggedInPath = this.route + '/ValidateToken';

  constructor(
    private router: Router,
    private http: HttpClient
  ) { }

  public isAuth = false;
  public isAdmin = false;
  public username: string;
  public role: string;
  isAuthChanged = new Subject<boolean>();

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
        this.role = tokenData["role"];
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

  setToken(token: string) {
    return localStorage.setItem(globalConstants.jwtTokenKey, token);
  }

  create(user: User) {
    return this.http.post(`${this.route}/Create`, user);
  }

  login(username: string, password: string) {
    return this.http.post(`${this.route}/Login`, { username, password });
  }

  logout() {
    localStorage.clear();
  }

  isLoggedIn(): Observable<boolean> {
    return this.http.get<boolean>(`${this.isLoggedInPath}`);
  }
}
