import { UserDashboardComponent } from "../user-dashboard/user-dashboard.component";
import { HomeComponent } from "../home/home.component";
import { LoginComponent } from "../login/login.component";
import { AdminDashboardComponent } from "../admin-dashboard/admin-dashboard.component";
import { AdminLoginComponent } from "../admin-login/admin-login.component";
import { SearchComponent } from "../search/search.component";
import { SignupComponent } from "../signup/signup.component";
import { BookFlightComponent } from "../book-flight/book-flight.component";
import { ManageBookingComponent } from "../manage-booking/manage-booking.component";
import { BookingHistoryComponent } from "../booking-history/booking-history.component";
//import { RegisterComponent } from "../register/register.component";
import { AuthGuardService } from "../services/auth-guard.service";
import { ManageScheduleComponent } from "../manage-schedule/manage-schedule.component";
import { ManageDiscountComponent } from "../manage-discount/manage-discount.component";

export const Mainroutes = [
  { path: '', component: HomeComponent },
  {path:'login',component: LoginComponent},
  {path:'register',component: SignupComponent},
  {path:'adminLogin',component: AdminLoginComponent},
  {path:'dashboard', component: UserDashboardComponent},
  {path:'admin', canActivate:[AuthGuardService], component: AdminDashboardComponent},
  {path:'admin/manage-schedule', canActivate:[AuthGuardService], component: ManageScheduleComponent},
  {path:'admin/manage-discounts', canActivate:[AuthGuardService], component: ManageDiscountComponent},
  { path: 'Home', component: HomeComponent },
  { path: 'search', component: SearchComponent },
  { path: 'book-flight/:id', component: BookFlightComponent },
  { path: 'manage-bookings', component: ManageBookingComponent },
  { path: 'booking-history', component: BookingHistoryComponent }
];


