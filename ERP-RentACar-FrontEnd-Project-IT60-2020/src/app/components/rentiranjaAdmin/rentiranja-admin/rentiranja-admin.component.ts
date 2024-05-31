import { Component } from '@angular/core';
import { Rentiranje } from 'src/app/models/rentiranje';
import { RentiranjeService } from 'src/app/service/rentiranje.service';

@Component({
  selector: 'app-rentiranja-admin',
  templateUrl: './rentiranja-admin.component.html',
  styleUrls: ['./rentiranja-admin.component.css']
})
export class RentiranjaAdminComponent {
  rentiranja: Rentiranje[] = [];
  currentPage: number = 1;
  pageSize: number = 5;
  totalRentiranja: number = 0;
  Math = Math; 

  constructor(private rentiranjeService: RentiranjeService) { }

  ngOnInit(): void {
    this.loadRentiranja();
  }

  loadRentiranja(): void {
    this.rentiranjeService.getRentiranja(this.currentPage, this.pageSize).subscribe(response => {
      this.rentiranja = response.rentiranja;
      this.totalRentiranja = response.totalCount;
    });
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.loadRentiranja();
  }

}
