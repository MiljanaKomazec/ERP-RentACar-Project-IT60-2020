import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { AUTOMOBIL_URL } from "../constants";
import { Automobil } from "../models/automobil";
import { Guid } from "guid-typescript";

@Injectable({
    providedIn: 'root'
  })
  export class AutomobilService{
    constructor(private httpClient: HttpClient) { }

      public getAllAutomobil(): Observable<any> {
          return this.httpClient.get(`${AUTOMOBIL_URL}`);
        }

      public getAutomobilById(id:Guid): Observable<any> {
          return this.httpClient.get(`${AUTOMOBIL_URL}/${id}`);
      }

      public addAutomobil(automobil:Automobil):Observable<any>{
        return this.httpClient.post(`${AUTOMOBIL_URL}` ,automobil);
      }
    
      public updateAutomobil(automobil:Automobil):Observable<any>{
        return this.httpClient.put(`${AUTOMOBIL_URL}` ,automobil);
      }
    
      public deleteAutomobil(id:Guid):Observable<any> {
        return this.httpClient.delete(`${AUTOMOBIL_URL}/${id}`);
      }

      public getAutomobilByTipMenjaca(tm:String): Observable<any> {
        return this.httpClient.get(`${AUTOMOBIL_URL}/tipMenjaca/${tm}`);
      }

      public getAutomobilByTipKaroserije(ta:String): Observable<any> {
        return this.httpClient.get(`${AUTOMOBIL_URL}/tipAutomobila/${ta}`);
      }

      public getAutomobilByTipMiK(tm:String, tk:String): Observable<any> {
        return this.httpClient.get(`${AUTOMOBIL_URL}/tipMiA/${tm}/${tk}`);
      }
  }