const DURATION_THRESHOLDS = {
  GOOD: 600, // 10m
  WARNING: 1200, // 20m
};

export function getDurationColor(
  time: number
): "green" | "orange" | "red" | "gray" {
  if (time <= 0) return "gray";
  if (time < DURATION_THRESHOLDS.GOOD) return "green";
  if (time < DURATION_THRESHOLDS.WARNING) return "orange";
  return "red";
}
