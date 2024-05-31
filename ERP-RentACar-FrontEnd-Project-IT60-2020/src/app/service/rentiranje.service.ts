import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RENTIRANJE_URL } from "../constants";
import { Rentiranje } from "../models/rentiranje";
import { Guid } from "guid-typescript";

@Injectable({
    providedIn: 'root'
  })
  export class RentiranjeService{
    constructor(private httpClient: HttpClient) { }

    getRentiranja(page: number, pageSize: number): Observable<{ rentiranja: Rentiranje[], totalCount: number }> {
      let params = new HttpParams()
        .set('page', page.toString())
        .set('pageSize', pageSize.toString());
  
      return this.httpClient.get<{ rentiranja: Rentiranje[], totalCount: number }>(`${RENTIRANJE_URL}`, { params });
    }
  

  public getAllRentiranje(): Observable<any> {
    return this.httpClient.get(`${RENTIRANJE_URL}`);
  }

  public addRentiranje(rentiranje:Rentiranje):Observable<any>{
    return this.httpClient.post(`${RENTIRANJE_URL}` ,rentiranje);
  }

  public updateRentiranje(rentiranje:Rentiranje):Observable<any>{
    return this.httpClient.put(`${RENTIRANJE_URL}/${rentiranje.rentiranjeId}` ,rentiranje);
  }

  public deleteRentiranje(id:Guid):Observable<any> {
    return this.httpClient.delete(`${RENTIRANJE_URL}/${id}`);
  }

  public dostupnostRentiranje(id:Guid):Observable<any> {
    return this.httpClient.get(`${RENTIRANJE_URL}/dostupnost/${id}`);
  }
}