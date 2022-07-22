import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FlightSearchService } from '../services/flight-search.service';
import { SearchResponse } from './search-response.model';
import { SearchRequest } from './search.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  today: any;
  showSpinner:boolean=false;
  modalText:string="";
  modalHeader:string="";
  searchRequest: SearchRequest = new SearchRequest();
  searchResult: any;
  searchResponse: SearchResponse[]=[];
  IsRoundTrip: boolean = false;
  minDate: Date = new Date();
  isSearchClicked: boolean = false;
  isNoResultFound: boolean = false;
  private _searchUrl="https://localhost:44380/api/FlightSearch/SearchFlight";

  constructor(private flightSearchService: FlightSearchService,private http:HttpClient,private _router:Router, private _auth: AuthService) { }

  ngOnInit(): void {
    this.today = new Date().toISOString().split('T')[0];
    this.today = this.today + 'T00:00';
  }

  toggleRoundTrip(input: boolean): void {
    this.IsRoundTrip = input;
    this.searchRequest.isRoundTrip = input;
  }


  onSearchClick() {
    console.log(this.searchRequest);
    let searchReq: any;
    if(this.IsRoundTrip){
      searchReq = {
        departure : this.searchRequest.fromCity,
        destination : this.searchRequest.toCity,
        departureDateTime : this.searchRequest.departureDate,
        returnDateTime : this.searchRequest.returnDate,
        isRoundTrip :  this.searchRequest.isRoundTrip
      }

    }
    else{
      searchReq = {
        departure : this.searchRequest.fromCity,
        destination : this.searchRequest.toCity,
        departureDateTime : this.searchRequest.departureDate,
        isRoundTrip :  this.searchRequest.isRoundTrip
      }
    }
    console.log(searchReq);
    this.ShowSpinner();
    this.flightSearchService.searchFlight(searchReq).subscribe((res: any) =>{ this.mapSearchResult(res),this.HideSpinner()},
      (    err: { error: string; })=>{this.HideSpinner(),this.DisplayModalPopup("Error", err.error)})
    //this.searchResult = this.flightSearchService.searchFlight(searchReq);
    console.log(this.searchResult);
    this.isSearchClicked = true;
  }

  mapSearchResult(res: any){
    this.searchResponse = res;
    if(this.searchResponse.length==0){
      this.isNoResultFound = true;
    }
    console.log(this.searchResponse);
  }

  hasError(typeofvalidator:string,controlname:string):Boolean{
    return this.searchRequest.formSearchGroup.controls[controlname].hasError(typeofvalidator);
  }

  DisplayModalPopup(modalHeader:string, modaltext:string)
{
  this.modalHeader = modalHeader;
  this.modalText=modaltext;
  document.getElementById("btnLaunchModal")?.click();
}

ShowSpinner()
{
  this.showSpinner = true;
}

HideSpinner()
{
  this.showSpinner = false;
}

  onBookClick(resp: SearchResponse) {
    var isLoggedIn = this._auth.loggedIn();
    if(isLoggedIn){
      this._router.navigate(['/book-flight/'+resp.flightInventoryId]);
    }
    else{
      this._router.navigate(['/login']);
    }
    
  }


}
