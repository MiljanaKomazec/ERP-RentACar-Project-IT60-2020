import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Automobil } from 'src/app/models/automobil';
import { Rentiranje } from 'src/app/models/rentiranje';
import { AuthenticateService } from 'src/app/service/authenticate.service';
import { AutomobilService } from 'src/app/service/automobil.service';
import { LogInDialogComponent } from '../dialog/logIn/log-in-dialog/log-in-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-rentiranje',
  templateUrl: './rentiranje.component.html',
  styleUrls: ['./rentiranje.component.css']
})
export class RentiranjeComponent {

  automobilId!: Guid;
  automobil!: Automobil;
  rentiranje!: Rentiranje;
  numberOfDays: number = 0;
  

  constructor(private route: ActivatedRoute,
    private automobilSerivce: AutomobilService,
    private router: Router,
    private authService: AuthenticateService,
    private dialog: MatDialog

  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const automobilIdString = params.get('automobilId'); // Dobijamo automobilId kao string
      if (automobilIdString) {
        this.automobilId = Guid.parse(automobilIdString); // Pretvaramo string u Guid
        console.log("ID automobila:", this.automobilId);
        // Ovdje možete izvršiti dodatne radnje koje zahtijevaju automobilId
      } else {
        console.error("Nedostaje automobilId u ruti."); // Obrada slučaja kada automobilId nije dostupan u ruti
      }
    });

    this.getAutomobil();
    this.rentiranje = new Rentiranje();

    
  }

  public getAutomobil():void{
    this.automobilSerivce.getAutomobilById(this.automobilId).subscribe(res =>{
      console.log('Fetched car:', res);
      this.automobil = res;
      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      console.error('Error fetching cars:', error);
    }
  }

  calculateNumberOfDays() {
    if (this.rentiranje.datumPocetkaIzdavanja && this.rentiranje.datumKrajaIzdavanja) {
      const startDate = new Date(this.rentiranje.datumPocetkaIzdavanja);
      const endDate = new Date(this.rentiranje.datumKrajaIzdavanja);
      const timeDifference = endDate.getTime() - startDate.getTime();
      const numberOfDays = Math.ceil(timeDifference / (1000 * 3600 * 24));
      this.rentiranje.brojDanaIzdavanja = numberOfDays;
      this.rentiranje.ukupnaCenaRentiranja = this.calculateTotalPrice();
      this.rentiranje.pristupniKod = this.generateRandomCode();
    }
  }
  calculateTotalPrice(): number {
    const cenaPoDanu = this.automobil.cenaPoDanu || 0; // Pretpostavka da postoji cijena po danu automobila
    const brojDana = this.rentiranje.brojDanaIzdavanja || 0;
    return cenaPoDanu * brojDana;
  }

  generateRandomCode(): number {
    // Generiranje random petocifrenog broja
    return Math.floor(10000 + Math.random() * 90000);
  }

  goBack() {
    this.router.navigate(['/automobili']); // Zamijenite '/' sa stvarnim putem do početne stranice ako je potrebno
  }


  rentiraj() {
    if (!this.authService.isLoggedIn) {
        this.openLoginDialog();
    } else {
        
    }
  }

  openLoginDialog() {
    this.dialog.open(LogInDialogComponent, {
        disableClose: true, 
    });
}
  
}
