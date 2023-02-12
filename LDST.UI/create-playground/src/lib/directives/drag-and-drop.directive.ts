import {
  Directive,
  EventEmitter,
  HostBinding,
  HostListener,
  Output,
} from '@angular/core';

@Directive({
  selector: '[ldstDragAndDrop]',
})
export class DragAndDropDirective {
  @HostBinding('class.file-over') fileOver!: boolean;
  @Output() fileDropped = new EventEmitter<File>();

  // Dragover listener
  @HostListener('dragover', ['$event']) onDragOver(evt: DragEvent): void {
    evt.preventDefault();
    evt.stopPropagation();
    this.fileOver = true;
  }

  // Dragleave listener
  @HostListener('dragleave', ['$event']) onDragLeave(evt: DragEvent): void {
    evt.preventDefault();
    evt.stopPropagation();
    this.fileOver = false;
  }

  // Drop listener
  @HostListener('drop', ['$event']) onDrop(evt: DragEvent): void {
    evt.preventDefault();
    evt.stopPropagation();
    this.fileOver = false;
    let acceptedFiles = [];

    if (!evt.dataTransfer) {
      return;
    }

    const { files } = evt.dataTransfer;
    acceptedFiles = Array.from(files);
    if (acceptedFiles.length > 0) {
      const lastFile = acceptedFiles.pop();
      this.fileDropped.emit(lastFile);
    }
  }
}
