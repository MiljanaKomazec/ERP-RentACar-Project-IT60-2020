import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Principal } from 'src/app/models/principal';
import { AuthenticateService } from 'src/app/service/authenticate.service';

@Component({
  selector: 'app-log-in-dialog',
  templateUrl: './log-in-dialog.component.html',
  styleUrls: ['./log-in-dialog.component.css']
})
export class LogInDialogComponent {

  principal!:Principal;

  constructor(private authService: AuthenticateService,
    public dialogRef: MatDialogRef<Principal>,
  ) { 
    this.principal = new Principal();
  }

  public logIn(): void {
    this.authService.logIn(this.principal).subscribe(res =>{
      console.log('Logged in successfully', res);
      this.principal = res;
      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      console.error('Login failed');
    }
  }

  public cancel():void{
    this.dialogRef.close();
  }

}
