import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ImageInfo } from '../../models/image-info.model';
import { v4 as uuidv4 } from 'uuid';
import { ImageValidators } from '../../services/image.validators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'ldst-picture-card',
  templateUrl: './picture-card.component.html',
  styleUrls: ['./picture-card.component.scss'],
})
export class PictureCardComponent {
  @Input() fileInfo!: ImageInfo;
  @Input() playgroundImagesForm!: FormArray;

  @Output() photoDeleting = new EventEmitter<string>();
  @Output() photoAdding = new EventEmitter<void>();

  uploadingRestricted = false;

  @ViewChild('fileDropRef', { static: false }) fileDropRef!: ElementRef;

  constructor(
    private readonly imageValidators: ImageValidators,
    private readonly toastr: ToastrService
  ) {}

  onBrowseFile() {
    this.fileDropRef.nativeElement.click();
  }

  onUpload(target: EventTarget): void {
    if (this.uploadingRestricted) {
      return;
    }

    const files = (target as HTMLInputElement).files;
    if (!files) {
      return;
    }

    this.onFileDropped(files[0]);
    this.fileDropRef.nativeElement.value = '';
  }

  onFileDropped(file: File): void {
    const id = uuidv4();
    this.playgroundImagesForm.push(
      new FormGroup({
        file: new FormControl(file, {
          validators: [
            this.imageValidators.checkSize(),
            this.imageValidators.checkFormat(),
          ],
        }),
        id: new FormControl(id),
      })
    );
    this.playgroundImagesForm.markAsDirty();

    if (this.playgroundImagesForm.valid) {
      this.setViewImageFromDataUrl(file);
      this.fileInfo.id = id;
      this.photoAdding.emit();
      this.uploadingRestricted = true;
    } else {
      this.toastr.warning(this.getErrorMessage());
    }
  }

  private getErrorMessage(): string {
    const fileControl = this.playgroundImagesForm.controls.at(-1)?.get('file');
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

  private setViewImageFromDataUrl(file: File): void {
    const fr = new FileReader();
    fr.onload = () => {
      this.fileInfo.fileUrl = fr.result;
    };

    fr.readAsDataURL(file);
  }

  onDeletePhoto(): void {
    this.photoDeleting.emit(this.fileInfo.id);
    const controlIndex = this.playgroundImagesForm.controls.findIndex(
      (c) => c.value.id === this.fileInfo.id
    );
    this.playgroundImagesForm.removeAt(controlIndex);
  }
}
