import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // private _registerUrl="https://localhost:44341/api/v1.0/flight/airline/register";
  // private _loginUrl="https://localhost:44341/api/v1.0/flight/airline/login";
  // private _adminLoginUrl="https://localhost:44341/api/v1.0/flight/admin/login";

  private _registerUrl="https://localhost:44341/api/v1.0/flight/airline/register";
  private _loginUrl="https://localhost:44341/api/v1.0/flight/airline/login";
  private _adminLoginUrl="https://localhost:44341/api/v1.0/flight/admin/login";

  constructor(private http:HttpClient,private _router:Router) { }

  loginUser(user:any)
  {
    return this.http.post<any>(this._loginUrl,user);
  }
  regsiterUser(user:any)
  {
    return this.http.post<any>(this._registerUrl,user);
  }
  logoutUser(){
    localStorage.removeItem('token');
    this._router.navigate(['/home']);
  }
  loggedIn(){
    return !!localStorage.getItem('token') && localStorage.getItem('usertype')=='user';
  }
  getToken(){
    return localStorage.getItem('token');
  }

  loginAdmin(user:any)
  {
    return this.http.post<any>(this._adminLoginUrl,user);
  }

  logoutAdmin(){
    localStorage.removeItem('token');
    this._router.navigate(['/home']);
  }

  adminLoggedIn(){
    return !!localStorage.getItem('token') && localStorage.getItem('usertype')=='admin';
  }

  RegisterUser(user: any){
    return this.http.post<any>(this._registerUrl,user);
  }
}
