import { Component, ViewChild } from '@angular/core';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { LogInDialogComponent } from '../../dialog/logIn/log-in-dialog/log-in-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticateService } from 'src/app/service/authenticate.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  @ViewChild(MatMenuTrigger) menu!: MatMenuTrigger;

  constructor(
    private dialog: MatDialog,
    public authService: AuthenticateService) {}

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

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn;
    
  }
  logout(): void {
    this.authService.doLogout();
    console.log(localStorage)
    console.log('isLoggedIn after logout:', this.authService.isLoggedIn);
  }

  isAdmin(): boolean {
    const currentUser = this.authService.decodeToken();  
    return currentUser.Role === 'Admin'; 
    
  }

}
