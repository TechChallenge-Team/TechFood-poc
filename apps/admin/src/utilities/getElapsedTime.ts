export function getElapsedTime(date: Date): number {
  const now = Date.now();
  return Math.floor((now - date.getTime()) / 1000);
}
