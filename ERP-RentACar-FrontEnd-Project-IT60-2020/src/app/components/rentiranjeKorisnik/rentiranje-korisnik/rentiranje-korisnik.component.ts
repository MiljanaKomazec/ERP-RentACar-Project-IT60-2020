import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Korisnik } from 'src/app/models/korisnik';
import { Rentiranje } from 'src/app/models/rentiranje';
import { KorisnikService } from 'src/app/service/korisnik.service';
import { RentiranjeService } from 'src/app/service/rentiranje.service';

@Component({
  selector: 'app-rentiranje-korisnik',
  templateUrl: './rentiranje-korisnik.component.html',
  styleUrls: ['./rentiranje-korisnik.component.css']
})
export class RentiranjeKorisnikComponent {
  rentiranja: Rentiranje[] = [];
  korisnikId!: Guid;
  korisnik!: Korisnik;

  constructor(private route: ActivatedRoute,
    private korisnikService: KorisnikService,
    private router: Router,
    private rentiranjeService: RentiranjeService
  ) { }


  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const korisnikId = params['korisnikId'];
      if (korisnikId) {
        this.korisnikId = korisnikId;
        console.log("Preneseni podaci:", this.korisnikId);
        this.loadRentiranja();
        this.getKorisnik();
      }
    });
  }

  loadRentiranja(): void {
    this.rentiranjeService.getKorisnikRentiranje(this.korisnikId).subscribe(response => {
      console.log('Fetched rentiranje:', response);
      this.rentiranja = response;
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
}
