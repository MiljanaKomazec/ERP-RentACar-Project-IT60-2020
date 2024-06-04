import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { loadStripe } from '@stripe/stripe-js';
import { RentiranjeComponent } from 'src/app/components/rentiranje/rentiranje.component';
import { Rentiranje } from 'src/app/models/rentiranje';
import { RentiranjeDTO } from 'src/app/models/rentiranjeDTO';
import { AuthenticateService } from 'src/app/service/authenticate.service';
import { RentiranjeService } from 'src/app/service/rentiranje.service';

@Component({
  selector: 'app-stripe-payment-dialog',
  templateUrl: './stripe-payment-dialog.component.html',
  styleUrls: ['./stripe-payment-dialog.component.css']
})
export class StripePaymentDialogComponent {
  stripe: any;
  card: any;
  rentiranje: RentiranjeDTO;

  constructor(
    private http: HttpClient,
    public dialogRef: MatDialogRef<StripePaymentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private rentiranjeService: RentiranjeService,
    public authService: AuthenticateService,
    private router: Router
  ) {
    this.rentiranje = data.rentiranje;
    console.log(this.rentiranje);
  }

  async ngOnInit() {
    this.initializeStripe();
  }

  async initializeStripe() {
    this.stripe = await loadStripe('pk_test_51PG5DH01Y3CG6EWF3UwPaMfE18grVJmKYSCyGfXUCA6cMSMFTR2BoAqJBCLmaRBcvOJ6oiLFonK5OKjjYZSJIhuO00dPn14STG');
    const elements = this.stripe.elements();
    this.card = elements.create('card');
    this.card.mount('#card-element');
  }

  async handleFormSubmit() {
    const { token, error } = await this.stripe.createToken(this.card);
    if (error) {
      console.error('Error:', error);
    } else {
      this.rentiranje.stripeToken = token.id;
      this.rentiranje.placenoR = "Ne";
      
      console.log(this.rentiranje);
      this.rentiranjeService.addRentiranje(this.rentiranje).subscribe({
        next: (response) => {
          console.log('Rent created:', response);
          this.dialogRef.close(true);
          const currentUser = this.authService.decodeToken();
          const korisnikId = currentUser.Id;
          console.log("Preneseni podaci:", korisnikId);
  
          this.router.navigate(['myRentiranja'], { queryParams: { korisnikId: korisnikId } });
        },
        error: (err) => {
          console.error('Payment error:', err);
          this.dialogRef.close(false);
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
