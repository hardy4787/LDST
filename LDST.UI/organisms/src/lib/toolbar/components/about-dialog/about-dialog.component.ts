import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'ldst-about-dialog',
  templateUrl: './about-dialog.component.html',
  styleUrls: ['./about-dialog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AboutDialogComponent {
  constructor(private readonly dialogRef: MatDialogRef<AboutDialogComponent>) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
