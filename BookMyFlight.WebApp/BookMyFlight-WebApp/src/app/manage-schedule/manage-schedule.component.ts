import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';
import { AddScheduleRequest } from './models/AddScheduleRequest';

@Component({
  selector: 'app-manage-schedule',
  templateUrl: './manage-schedule.component.html',
  styleUrls: ['./manage-schedule.component.css']
})
export class ManageScheduleComponent implements OnInit {


  showSpinner: boolean = false;
  modalText: string = "";
  modalHeader: string = "";
  addScheduleRequest: AddScheduleRequest = new AddScheduleRequest();
  today: any;
  constructor(private adminService: AdminService, private _router: Router) { }

  ngOnInit(): void {
    this.today = new Date().toISOString().split('T')[0];
    this.today = this.today + 'T00:00';
  }

  DisplayModalPopup(modalHeader: string, modaltext: string) {
    this.modalHeader = modalHeader;
    this.modalText = modaltext;
    document.getElementById("btnLaunchModal")?.click();
  }

  ShowSpinner() {
    this.showSpinner = true;
  }

  HideSpinner() {
    this.showSpinner = false;
  }

  HasError(typeofvalidator: string, controlname: string): Boolean {
    return this.addScheduleRequest.formScheduleAddGroup.controls[controlname].hasError(typeofvalidator);
  }

  AddScheduleInventory() {
    this.ShowSpinner();
    let addInventoryReq: any = {
      airlineName: this.addScheduleRequest.airlineName,
      airlineStatus: this.addScheduleRequest.airlineStatus,
      flightNumber: this.addScheduleRequest.flightNumber,
      departure: this.addScheduleRequest.departure,
      destination: this.addScheduleRequest.destination,
      departureDateTime: this.addScheduleRequest.departureDateTime,
      destinationDateTime: this.addScheduleRequest.destinationDateTime,
      scheduledDays: this.addScheduleRequest.scheduledDays,
      modelType: this.addScheduleRequest.modelType,
      noOfBusinessClassSeats: Number(this.addScheduleRequest.noOfBusinessClassSeats),
      noOfNonBusinessClassSeats: Number(this.addScheduleRequest.noOfNonBusinessClassSeats),
      ticketCost: Number(this.addScheduleRequest.ticketCost),
      numberOfRows: Number(this.addScheduleRequest.numberOfRows),
      meal: this.addScheduleRequest.meal
    }
    this.adminService.ScheduleFlight(addInventoryReq).subscribe(res=>{this.HideSpinner(),this.DisplayModalPopup('Success','Airline successfully added to inventory.')},
    err=>{this.HideSpinner(),this.DisplayModalPopup('Success','Airline successfully added to inventory.')});
    this.addScheduleRequest = new AddScheduleRequest();
  }

}
