import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Principal } from "../models/principal";
import { KORISNIK_URL } from "../constants";
import { Router } from "@angular/router";
import { TokenClass } from "../models/token";
import { jwtDecode } from "jwt-decode";
import { Observable, tap } from "rxjs";

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
        let removeToken = localStorage.removeItem('token');
        if (removeToken == null) {
        this.router.navigate(['login']); }
    }

    getToken() {
        return localStorage.getItem('token');
    }
    
    decodeToken() :TokenClass {
        let token = this.getToken();
        try {
        return jwtDecode(token!)
        } catch (Error) {
        return new TokenClass;
        }
    }
        
    get isLoggedIn(){
        let authToken = localStorage.getItem('token');
        return authToken !== null ? true : false;
    }
  }