import { Component } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Korisnik } from 'src/app/models/korisnik';
import { KorisnikService } from 'src/app/service/korisnik.service';
import { KorisnikUadDialogComponent } from '../../dialog/korisnikUpdateAndDelete/korisnik-uad-dialog/korisnik-uad-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-korisnici-admin',
  templateUrl: './korisnici-admin.component.html',
  styleUrls: ['./korisnici-admin.component.css']
})
export class KorisniciAdminComponent {

  korisnici: Korisnik[] = [];
  currentPage: number = 1;
  pageSize: number = 5;
  totalKorisnici: number = 0;
  Math = Math; 

  constructor(private korisnikService: KorisnikService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadKorisnici();
  }

  loadKorisnici(): void {
    this.korisnikService.getKorisnici(this.currentPage, this.pageSize).subscribe(response => {
      console.log(response);
      this.korisnici = response.korisnici;
      this.totalKorisnici = response.totalCount;
    });
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.loadKorisnici();
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
        this.loadKorisnici();
      }
    });
  }


}
