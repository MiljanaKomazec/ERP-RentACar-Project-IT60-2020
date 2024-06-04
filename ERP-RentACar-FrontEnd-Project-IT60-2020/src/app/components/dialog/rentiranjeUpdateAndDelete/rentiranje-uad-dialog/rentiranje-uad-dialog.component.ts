import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UpdateZaposleniDTO } from 'src/app/models/updateZaposleniDTO';
import { RentiranjeService } from 'src/app/service/rentiranje.service';

@Component({
  selector: 'app-rentiranje-uad-dialog',
  templateUrl: './rentiranje-uad-dialog.component.html',
  styleUrls: ['./rentiranje-uad-dialog.component.css']
})
export class RentiranjeUadDialogComponent {
  flag!:number;

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<UpdateZaposleniDTO>,
    @Inject(MAT_DIALOG_DATA) public data: UpdateZaposleniDTO,
    public rentiranjeService: RentiranjeService){

  }

  public update():void{
    this.rentiranjeService.updateRentiranjeZaposleni(this.data).subscribe(
      () => {
        this.snackBar.open('Uspešno promenjen zaposleni zadužen za rentiranje',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Greška prilikom ažuriranja korisnika',
      'ok', {duration:3500});
    }
  }
  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('Odustali ste od izmena',
      'ok', {duration:3500});
  }

}
