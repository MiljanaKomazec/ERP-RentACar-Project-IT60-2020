import { Component } from '@angular/core';
import { Korisnik } from 'src/app/models/korisnik';
import { KorisnikService } from 'src/app/service/korisnik.service';

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

  constructor(private korisnikService: KorisnikService) { }

  ngOnInit(): void {
    this.loadKorisnici();
  }

  loadKorisnici(): void {
    this.korisnikService.getKorisnici(this.currentPage, this.pageSize).subscribe(response => {
      this.korisnici = response.korisnici;
      this.totalKorisnici = response.totalCount;
    });
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.loadKorisnici();
  }

}
