import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Principal } from "../models/principal";
import { KORISNIK_URL } from "../constants";
import { Router } from "@angular/router";
import { TokenClass } from "../models/token";
import { jwtDecode } from "jwt-decode";
import { Observable, tap } from "rxjs";
import { Guid } from "guid-typescript";

@Injectable({
    providedIn: 'root'
  })
  export class AuthenticateService {
    constructor(private http: HttpClient, public router : Router) { }

    logIn(principal: Principal): Observable<any> {
        return this.http.post(`${KORISNIK_URL}/authenticate`, principal)
          .pipe(
            tap((res: any) => {
              localStorage.setItem('token', res.token);
            })
          );
    }
      

    doLogout() {
        return localStorage.removeItem('token');
        
    }

    getToken() {
        return localStorage.getItem('token');
    }
    
    decodeToken(): TokenClass {
      let token = this.getToken();
      const tokenObj = new TokenClass(); 
  
      try {
          const decodedToken: { [key: string]: string } = jwtDecode(token!);
          tokenObj.Role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || ''; 
          tokenObj.Id = decodedToken["id"] ? Guid.parse(decodedToken["id"]) : Guid.createEmpty(); 
      } catch (error) {
          console.error("Error decoding token:", error);
          
          return new TokenClass();
      }
  
      return tokenObj; 
  }
  
        
    get isLoggedIn(){
        let authToken = localStorage.getItem('token');
        return authToken !== null ? true : false;
    }
  }