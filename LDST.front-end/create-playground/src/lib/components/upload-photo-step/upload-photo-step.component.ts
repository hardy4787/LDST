import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import { PlaygroundStore } from '../../services/playground.store';

@UntilDestroy()
@Component({
  selector: 'ldst-upload-photo-step',
  templateUrl: './upload-photo-step.component.html',
  styleUrls: ['./upload-photo-step.component.scss'],
})
export class UploadPhotoStepComponent {
  file!: File | null;
  fileURL?: string | ArrayBuffer | null;

  @Input() form!: FormControl;
  @ViewChild('fileDropRef', { static: false }) fileDropRef!: ElementRef;

  constructor(private readonly playgroundStore: PlaygroundStore) {}

  onBrowseFile() {
    this.fileDropRef.nativeElement.click();
  }

  onUpload(target: EventTarget): void {
    const files = (target as HTMLInputElement).files;
    if (!files) {
      return;
    }

    this.onFileDropped(files[0]);
    this.fileDropRef.nativeElement.value = '';
  }

  onDeletePhoto(): void {
    this.form.setValue(null);
    this.fileURL = '';
    this.playgroundStore.clearTitleImage();
  }

  onFileDropped(file: File): void {
    this.form.setValue(file);
    this.form.markAsDirty();
    if (this.form.valid) {
      this.setViewImageFromDataUrl(file);
      this.playgroundStore.updateTitleImage(file);
    }
  }

  private setViewImageFromDataUrl(file: File): void {
    const fr = new FileReader();
    fr.onload = () => {
      this.fileURL = fr.result;
    };

    fr.readAsDataURL(file);
  }
}
