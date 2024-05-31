import { Component, ViewChild } from '@angular/core';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { LogInDialogComponent } from '../../dialog/logIn/log-in-dialog/log-in-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticateService } from 'src/app/service/authenticate.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SingUpDialogComponent } from '../../dialog/singUp/sing-up-dialog/sing-up-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  @ViewChild(MatMenuTrigger) menu!: MatMenuTrigger;

  constructor(
    public snackBar: MatSnackBar,
    private dialog: MatDialog,
    public authService: AuthenticateService,
    private router: Router) {}

  toggleMenu() {
    if (this.menu) {
      this.menu.openMenu();
    }
  }

  public openDialogLogIn(): void {
    const dialogRef = this.dialog.open(LogInDialogComponent, { });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.menu.openMenu();
        }
      }
    )
  }

  public openDialogSingUp(): void {
    const dialogRef = this.dialog.open(SingUpDialogComponent, { });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.menu.openMenu();
        }
      }
    )
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn;
    
  }
  logout(): void {
    this.authService.doLogout();
    this.snackBar.open('Korisnik je uspešno izlogovan', 'ok', { duration: 3500 });
    console.log(localStorage);
    console.log('isLoggedIn after logout:', this.authService.isLoggedIn);
  }
  

  isAdmin(): boolean {
    const currentUser = this.authService.decodeToken();  
    return currentUser.Role === 'Admin'; 
    
  }

  goToAllRentiranje() {
    this.router.navigate(['rentiranja']);
  }

  goToAllKorisnici() {
    this.router.navigate(['korisnici']);
  }

}
