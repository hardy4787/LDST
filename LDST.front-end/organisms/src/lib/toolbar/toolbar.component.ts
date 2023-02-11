import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthenticationStatusService } from '@ldst/shared';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AboutDialogComponent } from './components/about-dialog/about-dialog.component';
import { ContactsDialogComponent } from './components/contacts-dialog/contacts-dialog.component';

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
    private readonly changeDetectorRef: ChangeDetectorRef,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.authStatusService.authChanged.subscribe((res) => {
      this.isUserAuthenticated = res;
      this.changeDetectorRef.markForCheck();
    });
  }

  onOpenContactsDialog(): void {
    this.dialog
      .open(ContactsDialogComponent, {
        width: '328px',
        panelClass: 'mat-dialog-default-class',
      })
      .afterClosed()
      .pipe(untilDestroyed(this))
      .subscribe();
  }

  onOpenAboutDialog(): void {
    this.dialog
      .open(AboutDialogComponent, {
        width: '328px',
        panelClass: 'mat-dialog-default-class',
      })
      .afterClosed()
      .pipe(untilDestroyed(this))
      .subscribe();
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
