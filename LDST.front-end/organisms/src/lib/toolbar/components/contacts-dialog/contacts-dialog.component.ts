import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'ldst-contacts-dialog',
  templateUrl: './contacts-dialog.component.html',
  styleUrls: ['./contacts-dialog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ContactsDialogComponent {}
