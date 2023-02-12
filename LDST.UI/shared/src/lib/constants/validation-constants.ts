export class ValidationConstants {
  static readonly REQUIRED_MESSAGE = `Required.`;
  static readonly INCORRECT_EMAIL_MESSAGE = `Invalid email format.`;
  static readonly PASSWORDS_MUST_MATCH_MESSAGE =
    'Entered passwords must match.';
  static readonly incorrectSelectedFileType = (fileType: string): string =>
    `You have selected a filetype not allowed. Please select a ${fileType} file.`;
  static readonly maxCharLimitMessage = (limit: number): string =>
    `Exceeds character limit (${limit}).`;
}
