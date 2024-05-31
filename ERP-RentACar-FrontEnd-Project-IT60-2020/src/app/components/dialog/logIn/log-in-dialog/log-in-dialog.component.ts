import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Principal } from 'src/app/models/principal';
import { AuthenticateService } from 'src/app/service/authenticate.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-log-in-dialog',
  templateUrl: './log-in-dialog.component.html',
  styleUrls: ['./log-in-dialog.component.css']
})
export class LogInDialogComponent {

  principal!: Principal;

  constructor(public snackBar: MatSnackBar,
              private authService: AuthenticateService,
              public dialogRef: MatDialogRef<Principal>) {
    this.principal = new Principal();
  }

  public logIn(): void {
    this.authService.logIn(this.principal).pipe(
      catchError((error: Error) => {
        this.snackBar.open('Greška prilikom logovanja.', 'ok', { duration: 3500 });
        return of(null); // Return a default value or null to continue the observable chain
      })
    ).subscribe(res => {
      if (res) {
        console.log('Logged in successfully', res);
        this.principal = res;
        this.snackBar.open('Korisnik je uspešno ulogovan', 'ok', { duration: 3500 });
      }
    });
  }

  public cancel(): void {
    this.dialogRef.close();
  }
}
