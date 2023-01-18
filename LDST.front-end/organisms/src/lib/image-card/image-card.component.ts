import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ImageInfo, ImageValidators, SharedModule } from '@ldst/shared';
import { v4 as uuidv4 } from 'uuid';

@Component({
  imports: [CommonModule, SharedModule],
  providers: [ImageValidators],
  standalone: true,
  selector: 'ldst-image-card',
  templateUrl: './image-card.component.html',
  styleUrls: ['./image-card.component.scss'],
})
export class ImageCardComponent {
  @Input() fileInfo: ImageInfo = new ImageInfo();
  @Input() imageForm!: FormArray | FormControl;

  @Output() photoDeleted = new EventEmitter<ImageInfo>();
  @Output() photoAdded = new EventEmitter<ImageInfo>();
  @Output() photoNotAdded = new EventEmitter<void>();

  @ViewChild('fileDropRef', { static: false }) fileDropRef!: ElementRef;

  constructor(private readonly imageValidators: ImageValidators) {}

  onBrowseFile() {
    this.fileDropRef.nativeElement.click();
  }

  onUpload(event: Event): void {
    const files = (event.target as HTMLInputElement).files;
    if (!files) {
      return;
    }

    this.onFileDropped(files[0]);
    this.fileDropRef.nativeElement.value = '';
  }

  onFileDropped(file: File | any): void {
    const id = uuidv4();

    if (this.imageForm instanceof FormArray) {
      this.imageForm.push(
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
    }

    if (this.imageForm instanceof FormControl) {
      this.imageForm.setValue(file);
    }

    this.imageForm.markAsDirty();

    if (this.imageForm.valid) {
      this.setViewImageFromDataUrl(file);
      this.fileInfo.id = id;
      this.fileInfo.file = file;
      this.photoAdded.emit(this.fileInfo);
    } else {
      this.photoNotAdded.emit();
    }
  }

  onDeletePhoto(event: Event): void {
    event.stopImmediatePropagation();
    this.photoDeleted.emit(this.fileInfo);
  }

  private setViewImageFromDataUrl(file: File): void {
    const fr = new FileReader();
    fr.onload = () => {
      this.fileInfo.fileUrl = fr.result;
    };

    fr.readAsDataURL(file);
  }
}
