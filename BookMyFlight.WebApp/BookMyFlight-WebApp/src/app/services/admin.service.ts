import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private _addInventoryUrl = "https://localhost:44341/api/v1.0/flight/airline/inventory/add";
  constructor(private http:HttpClient,private _router:Router) { }

  ScheduleFlight(addScheduleRequest: any){

    return this.http.post<any>(this._addInventoryUrl,addScheduleRequest);

  }
}
