# T9C – Data in the Real World: Working with Files

## Files

| File | Description |
|------|--------------|
| `dirty_data_practice.csv` | Guided practice dataset with multiple issues (missing values, inconsistent formats, invalid numbers). |

| `dirty_data_practice.csv` | Challenge dataset (use same file but solve independently with all issues). |
| `README.md` | Instructions for completing both activities. |

---

## Activity 1: Guided Practice 

### Goal
Work in pairs to clean a dataset with **intentional data quality issues**.

### Steps
1. Open `dirty_data_practice.csv`.

2. Identify the problems:
   - Inconsistent date formats.
   - Invalid and mixed `WatchTime` values (text, negative, missing).
   - Inconsistent capitalization in `Name` and `Team`.
   - Missing values in multiple columns.

3. Fix the issues by creating a .cs file:
   - Convert `WatchTime` into numeric minutes.
   - Standardize all dates to `YYYY-MM-DD`.
   - Standardize text formatting (capitalize names, normalize teams).
   - Handle missing values appropriately.

4. Save your cleaned version as `clean_video_data.csv`.

## Activity 1: Application Challenge

### Goal
Clean the dataset independently and prepare it for analysis.

### Steps
1. Open `dirty_data_practice.csv`.

2. Perform full cleaning:
   - Ensure all columns have correct data types.
   - Remove or fix invalid entries.
   - Normalize all categorical values.
   - Ensure no missing critical fields.

3. Create new insights:
   - Total watch time per team.
   - Most watched videos.
   - Average watch time per user.

4. Save:
   - `clean_video_data.csv`
   - `clean_video_data.json`

5. Document your process in a **Data Cleaning Log**.

---

## Stretch Task: Validate JSON Schema

If you finish early, validate your cleaned JSON using this schema:

```json
{
  "type": "array",
  "items": {
    "type": "object",
    "properties": {
      "UserID": { "type": "integer" },
      "Name": { "type": "string" },
      "Team": { "type": "string" },
      "VideoWatched": { "type": ["string", "null"] },
      "DateWatched": { "type": "string", "pattern": "^\\d{4}-\\d{2}-\\d{2}$" },
      "WatchTime": { "type": ["number", "null"] },
      "Feedback": { "type": ["string", "null"] }
    },
    "required": ["UserID", "Name", "Team", "DateWatched"]
  }
}
```