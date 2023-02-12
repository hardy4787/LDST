import { Component, OnInit } from '@angular/core';
import { Claim } from './models/claim.model';
import { PrivacyService } from './services/privacy.service';

@Component({
  selector: 'ldst-privacy-page',
  templateUrl: './privacy-page.component.html',
  styleUrls: ['./privacy-page.component.scss'],
})
export class PrivacyPageComponent implements OnInit {
  claims = [] as Claim[];
  constructor(private readonly privacyService: PrivacyService) {}

  ngOnInit(): void {
    this.getClaims();
  }

  getClaims(): void {
    this.privacyService.getClaims$().subscribe((claims) => {
      this.claims = claims;
    });
  }
}
