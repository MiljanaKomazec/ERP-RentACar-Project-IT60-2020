import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, of } from 'rxjs';
import { Korisnik } from 'src/app/models/korisnik';
import { KorisnikService } from 'src/app/service/korisnik.service';

@Component({
  selector: 'app-sing-up-dialog',
  templateUrl: './sing-up-dialog.component.html',
  styleUrls: ['./sing-up-dialog.component.css']
})
export class SingUpDialogComponent {

  korisnik!: Korisnik;

  constructor(public snackBar: MatSnackBar,
              private korisnikService: KorisnikService,
              public dialogRef: MatDialogRef<Korisnik>) {
    this.korisnik = new Korisnik();
  }

  public singUp(): void {
    this.korisnikService.addKorisnik(this.korisnik).pipe(
      catchError((error: Error) => {
        this.snackBar.open('Greška prilikom registracije.', 'ok', { duration: 3500 });
        return of(null); 
      })
    ).subscribe(res => {
      if (res) {
        console.log('Logged in successfully', res);
        this.korisnik = res;
        this.snackBar.open('Korisnik je uspešno registrovan', 'ok', { duration: 3500 });
      }
    });
  }



  public cancel(): void {
    this.dialogRef.close();
  }

}
