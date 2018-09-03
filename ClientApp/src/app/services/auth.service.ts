import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import * as auth0 from 'auth0-js';
import { JwtHelper } from 'angular2-jwt';


(window as any).global = window;

@Injectable()
export class AuthService {

  private roles: string[] = [];
  profile: any;

  auth0 = new auth0.WebAuth({
    clientID: 'uVcQ7NaOlw55P857aVoOqE8UkIpRRtmr',
    domain: 'hereglish.eu.auth0.com',
    responseType: 'token id_token',
    audience: 'https://hereglish.eu.auth0.com/userinfo',
    redirectUri: 'https://localhost:5001',
    scope: 'openid email profile'
  });

  constructor(
    public router: Router) {
    this.readUserFromLocalStorage();
  }

  public isInRole(roleName) {
    return this.roles.indexOf(roleName) > -1;
  }

  public unauthorizedAccess(){
    this.router.navigate(['unauthorized-access']);
  }

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): void {
    this.auth0.parseHash((err, authResult) => {
      if (err) {
        this.router.navigate(['']);
        return;
      }

      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';
        this.setSession(authResult);

        this.auth0.client.userInfo(authResult.accessToken, (err, profile) => {
          if (err) {
            throw err
          }

          localStorage.setItem('profile', JSON.stringify(profile));
          this.readUserFromLocalStorage();
        });

        this.router.navigate(['']);
      }
    });
  }

  private readUserFromLocalStorage() {
    this.profile = JSON.parse(localStorage.getItem('profile'));

    var token = localStorage.getItem('token');
    if (token) {
      var jwtHelper = new JwtHelper();
      var decodedToken = jwtHelper.decodeToken(token);
      this.roles = decodedToken['https://herenglish.eu.com/roles'] || [];
    }
  }

  private setSession(authResult): void {
    // Set the time that the Access Token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('profile');
    this.roles = [];

    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // Access Token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at') || '{}');
    return new Date().getTime() < expiresAt;
  }

}