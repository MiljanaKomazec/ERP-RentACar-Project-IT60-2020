import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Automobil } from 'src/app/models/automobil';
import { AutomobilService } from 'src/app/service/automobil.service';

@Component({
  selector: 'app-automobil-dialog',
  templateUrl: './automobil-dialog.component.html',
  styleUrls: ['./automobil-dialog.component.css']
})
export class AutomobilDialogComponent {

  flag!:number;

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Automobil>,
    @Inject(MAT_DIALOG_DATA) public data: Automobil,
    public automobilService: AutomobilService){

  }

  public add():void{
    this.automobilService.addAutomobil(this.data).subscribe(
      () => {
        this.snackBar.open('Automobil: ' + this.data.markaAutomobila + " " + this.data.modelAutomobila
         + ' uspešno kreiran',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom dodavanja automobila',
      'ok', {duration:3500});
    }
  }

  public update():void{
    this.automobilService.updateAutomobil(this.data).subscribe(
      () => {
        this.snackBar.open('Automobil: ' + this.data.markaAutomobila + " " + this.data.modelAutomobila
         + ' uspešno ažuriran',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom ažuriranje automobila',
      'ok', {duration:3500});
    }
  }

  public delete():void{
    this.automobilService.deleteAutomobil(this.data.automobilId).subscribe(
      () => {
        this.snackBar.open('Automobil: ' + this.data.markaAutomobila + " " + this.data.modelAutomobila
         + ' uspešno obrisan',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom brisanja automobila',
      'ok', {duration:3500});
    }
  }

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('Odustali ste od izmena',
      'ok', {duration:3500});
  }

}
