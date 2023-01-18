import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationStatusService } from '@ldst/shared';
import { UntilDestroy } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'ldst-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ToolbarComponent implements OnInit {
  isUserAuthenticated!: boolean;

  constructor(
    private readonly router: Router,
    private readonly authStatusService: AuthenticationStatusService,
    private readonly changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.authStatusService.authChanged.subscribe((res) => {
      this.isUserAuthenticated = res;
      this.changeDetectorRef.markForCheck();
    });
  }

  onNavigateToCreatePlayground(): void {
    this.router.navigate(['/create-playground']);
  }

  onLogout(): void {
    this.authStatusService.logout();
    this.router.navigate(['/']);
  }

  onNavigateToPrivacyPage(): void {
    this.router.navigate(['/privacy']);
  }

  onNavigateToLoginPage(): void {
    this.router.navigate(['/authentication/login']);
  }

  onNavigateToProfilePage(): void {
    this.router.navigate([`/profile/${localStorage.getItem('userName')}`]);
  }
}
