import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Rentiranje } from 'src/app/models/rentiranje';
import { RentiranjeService } from 'src/app/service/rentiranje.service';

@Component({
  selector: 'app-rentiranje-dostupnost-dialog',
  templateUrl: './rentiranje-dostupnost-dialog.component.html',
  styleUrls: ['./rentiranje-dostupnost-dialog.component.css']
})
export class RentiranjeDostupnostDialogComponent {

  rentiranja!:Rentiranje[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Rentiranje>,
    @Inject(MAT_DIALOG_DATA) public data: Rentiranje,
    public rentiranjeService: RentiranjeService
  ) {}

  ngOnInit(): void {
    this.getRentiranja();
  }


  public getRentiranja():void{
    this.rentiranjeService.dostupnostRentiranje(this.data.automobilId).subscribe(res =>{
      console.log('Fetched comments:', res);
      this.rentiranja = res;
        this.snackBar.open('Rentiranja za automobil: ' + this.data.automobil.markaAutomobila + ' uspešno isčitana',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom isčitavanja.',
      'ok', {duration:3500});
    }
  }


}
