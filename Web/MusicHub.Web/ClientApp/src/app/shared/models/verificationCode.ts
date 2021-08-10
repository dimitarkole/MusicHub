import { VerificationType } from "./verificationType";

export default interface VerificationCode {
  code: string;
  type: VerificationType;
}
