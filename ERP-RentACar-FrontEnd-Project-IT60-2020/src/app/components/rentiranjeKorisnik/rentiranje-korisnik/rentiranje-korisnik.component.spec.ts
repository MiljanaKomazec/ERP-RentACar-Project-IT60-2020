import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentiranjeKorisnikComponent } from './rentiranje-korisnik.component';

describe('RentiranjeKorisnikComponent', () => {
  let component: RentiranjeKorisnikComponent;
  let fixture: ComponentFixture<RentiranjeKorisnikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentiranjeKorisnikComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentiranjeKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
