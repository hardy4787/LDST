import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedModule } from '@ldst/shared';

@Component({
  standalone: true,
  selector: 'ldst-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.scss'],
  imports: [SharedModule],
})
export class ForbiddenComponent implements OnInit {
  private returnUrl!: string;
  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public navigateToLogin = () => {
    this.router.navigate(['/authentication/login'], {
      queryParams: { returnUrl: this.returnUrl },
    });
  };
}
