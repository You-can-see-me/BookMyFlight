import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { MasterComponent } from './master.component';
import { HomeComponent } from '../home/home.component';
import { UserDashboardComponent } from '../user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from '../admin-dashboard/admin-dashboard.component';
import { LoginComponent } from '../login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Mainroutes } from '../app-routings/main-routes';
import { SearchComponent } from '../search/search.component';
import { SignupComponent } from '../signup/signup.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from '../services/auth.service';
import { TokenInterceptorService } from '../services/token-interceptor.service';
import { AdminLoginComponent } from '../admin-login/admin-login.component';
import { BookFlightComponent } from '../book-flight/book-flight.component';
import { ManageBookingComponent } from '../manage-booking/manage-booking.component';
import { BookingHistoryComponent } from '../booking-history/booking-history.component';
import { ManageScheduleComponent } from '../manage-schedule/manage-schedule.component';
import { ManageDiscountComponent } from '../manage-discount/manage-discount.component';

@NgModule({
  declarations: [
    MasterComponent,
    HomeComponent,
    UserDashboardComponent,
    AdminDashboardComponent,
    LoginComponent,
    SearchComponent,
    SignupComponent,
    AdminLoginComponent,
    BookFlightComponent,
    ManageBookingComponent,
    BookingHistoryComponent,
    ManageScheduleComponent,
    ManageDiscountComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(Mainroutes),
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    
  ],
  providers: [AuthService, { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true }],
  bootstrap: [MasterComponent],
})
export class MasterModule { }
