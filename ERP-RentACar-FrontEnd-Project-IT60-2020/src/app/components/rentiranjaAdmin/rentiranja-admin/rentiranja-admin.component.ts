import { Component } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Rentiranje } from 'src/app/models/rentiranje';
import { RentiranjeService } from 'src/app/service/rentiranje.service';
import { RentiranjeUadDialogComponent } from '../../dialog/rentiranjeUpdateAndDelete/rentiranje-uad-dialog/rentiranje-uad-dialog.component';
import { MatDialog } from '@angular/material/dialog';

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

  constructor(private rentiranjeService: RentiranjeService,
    public dialog: MatDialog
  ) { }

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

  public openDialog(
    flag: number,
    rentiranjeId?: Guid

  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(RentiranjeUadDialogComponent, {
      data: { rentiranjeId },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.loadRentiranja();
      }
    });
  }

}
