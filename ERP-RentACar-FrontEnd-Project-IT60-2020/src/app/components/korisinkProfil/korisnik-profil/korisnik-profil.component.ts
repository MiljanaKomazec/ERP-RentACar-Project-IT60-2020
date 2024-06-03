import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Korisnik } from 'src/app/models/korisnik';
import { AuthenticateService } from 'src/app/service/authenticate.service';
import { KorisnikService } from 'src/app/service/korisnik.service';
import { KorisnikUadDialogComponent } from '../../dialog/korisnikUpdateAndDelete/korisnik-uad-dialog/korisnik-uad-dialog.component';

@Component({
  selector: 'app-korisnik-profil',
  templateUrl: './korisnik-profil.component.html',
  styleUrls: ['./korisnik-profil.component.css']
})
export class KorisnikProfilComponent {
  korisnikId!: Guid;
  korisnik!: Korisnik;

  constructor(private route: ActivatedRoute,
    private korisnikService: KorisnikService,
    private router: Router,
    private dialog: MatDialog

  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const korisnikId = params['korisnikId'];
      if (korisnikId) {
        this.korisnikId = korisnikId;
        console.log("Preneseni podaci:", this.korisnikId);
        this.getKorisnik();
      }
    });
  }

  public getKorisnik():void{
    this.korisnikService.getKorisnikById(this.korisnikId).subscribe(res =>{
      console.log('Fetched korisnik:', res);
      this.korisnik = res;
      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      console.error('Error fetching korisnik:', error);
    }
  }

  public openDialog(
    flag: number,
    korisnikId?: Guid,
    imeK?: String,
    przK?: String,
    jmbgK?: String,
    gradK?: String,
    adresaK?: String,
    kontaktK?: String,
    uloga?: String,
    userNameK?: String,
    passwordK?: String

  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(KorisnikUadDialogComponent, {
      data: { korisnikId, imeK, przK, jmbgK, gradK, adresaK, kontaktK, uloga, userNameK, passwordK },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.getKorisnik();
      }
    });
  }

}
