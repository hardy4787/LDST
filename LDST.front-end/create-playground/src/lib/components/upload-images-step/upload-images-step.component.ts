import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormArray, FormControl } from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import { ToastrService } from 'ngx-toastr';
import { ImageInfo } from '../../models/image-info.model';
import { PlaygroundStore } from '../../services/playground.store';

@UntilDestroy()
@Component({
  selector: 'ldst-upload-images-step',
  templateUrl: './upload-images-step.component.html',
  styleUrls: ['./upload-images-step.component.scss'],
})
export class UploadImagesStepComponent {
  titleImage: ImageInfo = { id: '-1', fileUrl: null, file: null };
  galleryImages: ImageInfo[] = [{ id: '-1', fileUrl: null, file: null }];

  @Input() titleImageForm!: FormControl;
  @Input() galleryImagesForm!: FormArray;

  constructor(
    private readonly playgroundStore: PlaygroundStore,
    private readonly toastr: ToastrService
  ) {}

  onDeleteTitleImage(): void {
    const noFile = null;
    this.titleImage = { id: '-1', fileUrl: null, file: noFile };
    this.titleImageForm.setValue(noFile);
    this.playgroundStore.resetTitleImage();
  }

  onDeleteGalleryImage({ id }: ImageInfo): void {
    this.galleryImages = this.galleryImages.filter((f) => f.id !== id);
    const controlIndex = this.galleryImagesForm.controls.findIndex(
      (c) => c.value.id === id
    );
    this.galleryImagesForm.removeAt(controlIndex);
    this.playgroundStore.removeGalleryImage(id);
  }

  onAddTitleImage({ file }: ImageInfo) {
    this.playgroundStore.updateTitleImage(file as File);
  }

  onAddGalleryImage(event: ImageInfo): void {
    this.playgroundStore.addGalleryImage(event);
    this.addImageContainer();
  }

  onShowErrorMessage(): void {
    this.toastr.warning(this.getErrorMessage());
  }

  private addImageContainer(): void {
    this.galleryImages.push({
      id: '-1',
      fileUrl: null,
      file: null,
    });
  }

  private getErrorMessage(): string {
    const fileControl = this.galleryImagesForm.controls.at(-1)?.get('file');
    if (fileControl?.hasError('exceedSize')) {
      return 'File size may not exceed 5 MB.';
    }
    if (fileControl?.hasError('zeroSize')) {
      return 'Cannot upload 0KB sized files.';
    }
    if (fileControl?.hasError('incorrectType')) {
      return 'Cannot upload this file type.';
    }

    return '';
  }
}
