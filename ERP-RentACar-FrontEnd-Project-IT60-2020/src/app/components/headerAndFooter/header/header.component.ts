import { Component, ViewChild } from '@angular/core';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { LogInDialogComponent } from '../../dialog/logIn/log-in-dialog/log-in-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  @ViewChild(MatMenuTrigger) menu!: MatMenuTrigger;

  constructor(
    private dialog: MatDialog) {}

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

}
