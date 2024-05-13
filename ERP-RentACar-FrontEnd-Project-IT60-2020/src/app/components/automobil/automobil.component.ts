import { Component, ViewChild } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Automobil } from 'src/app/models/automobil';
import { AutomobilService } from 'src/app/service/automobil.service';
import { MatDialog } from '@angular/material/dialog';
import { RentiranjeDostupnostDialogComponent } from './dialogs/rentiranje-dostupnost-dialog/rentiranje-dostupnost-dialog.component';
import { NavigationExtras, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-automobil',
  templateUrl: './automobil.component.html',
  styleUrls: ['./automobil.component.css']
})
export class AutomobilComponent {

  automobili!:Automobil[];
  selectedDate!:Date;
  filterForm!: FormGroup;
  sortOrder: string = 'rastuce'; 
  pageIndex = 0;
  pageSize = 6;

   


  constructor(
    private automobilSerivce: AutomobilService,
      public dialog:MatDialog,
      private router: Router,
      private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      tipMenjaca: [''] ,
      tipKaroserije: ['']
    });

    
    this.filterForm.valueChanges.subscribe(value => {
      this.filtrirajAutomobile(value.tipMenjaca, value.tipKaroserije);
    });

    
    this.getAutomobil();
  }

  selectedAutomobil: Automobil | null = null;

  showDetails(automobil: Automobil) {
    this.selectedAutomobil = automobil;
  }
  goToRentiranje(automobilId : Guid) {
      console.log("Preneseni podaci:", automobilId);

      const navigationExtras: NavigationExtras = {
        state: {
          automobilId: automobilId,
        }
      };
    this.router.navigate(['rentiranje', automobilId]);
  }

  
  public getAutomobil():void{
    this.automobilSerivce.getAllAutomobil().subscribe(res =>{
      console.log('Fetched cars:', res);
      this.automobili = res;
      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      console.error('Error fetching cars:', error);
    }
  }

  filtrirajAutomobile(tipMenjaca: string, tipKaroserije: string) {
    if (!tipMenjaca && !tipKaroserije) {
      this.getAutomobil();
      return;
    }

    if (tipMenjaca && !tipKaroserije) {
      this.automobilSerivce.getAutomobilByTipMenjaca(tipMenjaca).subscribe(res => {
        this.automobili = res;
      });
      return;
    }

    if (!tipMenjaca && tipKaroserije) {
      this.automobilSerivce.getAutomobilByTipKaroserije(tipKaroserije).subscribe(res => {
        this.automobili = res;
      });
      return;
    }

    if (tipMenjaca && tipKaroserije) {
      this.automobilSerivce.getAutomobilByTipMiK(tipMenjaca,tipKaroserije).subscribe(res => {
        this.automobili = res;
      });
      return;
    }
    
  }

  resetujFilter() {
    
    this.filterForm.patchValue({ tipMenjaca: '', tipKaroserije: '' });
  }

  public openDialog(automobilId?:Guid):void{
    const dialogRef = this.dialog.open(RentiranjeDostupnostDialogComponent, {data:{automobilId}});
    dialogRef.afterClosed().subscribe(
      result =>{
        if(result==1){
          this.getAutomobil();
        }
      }
    )

  }

  public applyFilter(event:any){
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();

    if (!filterValue) {
      this.getAutomobil(); // Resetuj prikaz na sve automobile
      return;
    }

    this.automobili = this.automobili.filter(automobil =>
      automobil.markaAutomobila.toLowerCase().includes(filterValue)
    );

  }

  

  public sortiraj(sortOrder: string) {
    if (this.sortOrder === sortOrder) {
      return; 
    }
    this.sortOrder = sortOrder;

    if (sortOrder === 'rastuce') {
      this.automobili.sort((a, b) => a.cenaPoDanu - b.cenaPoDanu);
    } else if (sortOrder === 'opadajuce') {
      this.automobili.sort((a, b) => b.cenaPoDanu - a.cenaPoDanu);
    }
  }

  getDisplayedAutomobili() {
    const startIndex = this.pageIndex * this.pageSize;
    return this.automobili.slice(startIndex, startIndex + this.pageSize);
  }

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }




}
