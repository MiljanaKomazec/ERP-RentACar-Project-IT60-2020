import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AutomobilComponent } from './components/automobil/automobil.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatNativeDateModule} from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { RentiranjeDostupnostDialogComponent } from './components/automobil/dialogs/rentiranje-dostupnost-dialog/rentiranje-dostupnost-dialog.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RentiranjeComponent } from './components/rentiranje/rentiranje.component';
import { HeaderComponent } from './components/headerAndFooter/header/header.component';
import { FooterComponent } from './components/headerAndFooter/footer/footer.component';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule} from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatRadioModule } from '@angular/material/radio';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AuthInterceptorInterceptor } from './interceptor/auth-interceptor.interceptor';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { LogInDialogComponent } from './components/dialog/logIn/log-in-dialog/log-in-dialog.component';
import { RentiranjaAdminComponent } from './components/rentiranjaAdmin/rentiranja-admin/rentiranja-admin.component';
import { SingUpDialogComponent } from './components/dialog/singUp/sing-up-dialog/sing-up-dialog.component';
import { KorisniciAdminComponent } from './components/korisniciAdmin/korisnici-admin/korisnici-admin.component';
import { RentiranjeUadDialogComponent } from './components/dialog/rentiranjeUpdateAndDelete/rentiranje-uad-dialog/rentiranje-uad-dialog.component';


const appRoutes: Routes = [
  { path: 'automobili', component:AutomobilComponent },
  { path: 'rentiranje/:automobilId', component:RentiranjeComponent },
  {path:'', redirectTo:'/automobili', pathMatch:'full'},
  { path: 'rentiranja', component:RentiranjaAdminComponent},
  { path: 'korisnici', component:KorisniciAdminComponent}
  
];


@NgModule({
  declarations: [
    AppComponent,
    AutomobilComponent,
    RentiranjeDostupnostDialogComponent,
    RentiranjeComponent,
    HeaderComponent,
    FooterComponent,
    LogInDialogComponent,
    RentiranjaAdminComponent,
    SingUpDialogComponent,
    KorisniciAdminComponent,
    RentiranjeUadDialogComponent,

  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSortModule,
    MatInputModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatPaginatorModule,
    MatIconModule,
    MatMenuModule
    
   

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
