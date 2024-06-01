import { KorisnikService } from 'src/app/service/korisnik.service';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Korisnik } from 'src/app/models/korisnik';

@Component({
  selector: 'app-korisnik-uad-dialog',
  templateUrl: './korisnik-uad-dialog.component.html',
  styleUrls: ['./korisnik-uad-dialog.component.css']
})
export class KorisnikUadDialogComponent {

  flag!:number;

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Korisnik>,
    @Inject(MAT_DIALOG_DATA) public data: Korisnik,
    public korisnikService: KorisnikService){

  }

  public update():void{
    this.korisnikService.updateKorisnik(this.data).subscribe(
      () => {
        this.snackBar.open('Korisnik sa imenom i prezimenom: ' + this.data.imeK + " " + this.data.przK
         + ' uspešno ažuriran',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom ažuriranja korisnika',
      'ok', {duration:3500});
    }
  }

  public delete():void{
    this.korisnikService.deleteKorisnik(this.data.korisnikId).subscribe(
      () => {
        this.snackBar.open('Korisnik uspešno obrisan',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom brisanja korisnika',
      'ok', {duration:3500});
    }
  }

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('Odustali ste od izmena',
      'ok', {duration:3500});
  }
}
