# TodoList.Web — Frontend Guide

Blazor WebAssembly SPA. Talks to the TodoList API over HTTP.
This file scopes the **frontend only** — don't edit API code from here.

## Run
```powershell
dotnet watch run --project TodoList.Web    # hot reload
```
API must be running separately at https://localhost:7118
(`HttpClient.BaseAddress` is set in `Program.cs`).

## API contract (what the frontend calls)
All under `api/todoitems`, JSON, enums as strings.

- `GET    /api/todoitems`             → `List<TodoItemResponseDto>`
- `GET    /api/todoitems/{id}`        → `TodoItemResponseDto`
- `POST   /api/todoitems`             → create (body: CreateTodoItemDto)
- `PUT    /api/todoitems/{id}`        → full update
- `PUT    /api/todoitems/{id}/status` → change status (body: `{ "status": "Done" }`)
- `DELETE /api/todoitems/{id}`        → delete

**TodoItemResponseDto:** Id, TodoItemName, Description, Tags[] `{Id,TagName}`,
StartDate?, DueDate?, PriorityId?, PriorityName?, ReferenceNote, Status, CreatedAt, UpdatedAt?

**Status values:** `Pending` | `InProgress` | `Done` | `Overdue`
(Overdue is derived from DueDate < today — compute it in the UI, don't rely on it being stored.)

All HTTP calls go through `Services/TodoApiService.cs` (interface `ITodoApiService`).
Add new API methods there, never call `HttpClient` directly from a page.

## Design system — "studylist" (dark)
- Background `#131316` · surface `#1a1a1f` · text `#ececf1`
- Accent purple `#a996f2` · done green `#7bd8b0` · overdue red `#f28b8b` · muted `rgba(255,255,255,.4)`
- Fonts: Instrument Sans (UI), JetBrains Mono (dates/meta) — loaded in `wwwroot/index.html`
- Radius ~8–10px, subtle borders `rgba(255,255,255,.06–.12)`
- Reference prototype: `docs/design/studylist.html`

Views to build: **Board** (Overdue / To do / In progress / Done columns) → List → Week → Stats,
plus a task-detail modal and a task composer modal.

## Component conventions
- Pages in `Pages/`, one route each (`@page`). Full-screen pages use `@layout BlankLayout`
  to escape the default sidebar (`MainLayout`).
- Co-locate styles in `X.razor.css` (scoped). Global bits (fonts, resets) go in `wwwroot/index.html` / `css/app.css`.
- Keep components small; lift shared state into the page, pass down via parameters.

## Working style — vibe coding
- Write whole components; iterate from screenshots, not line-by-line.
- Match the design system colors/fonts above exactly.
- After a change, assume hot reload is on — just describe what to look at.
- Keep all data access in `TodoApiService`; UI stays declarative.
- When a frontend task is finished **and verified working**, append a short dated entry
  to the **Done log** at the bottom of this file (auto-import it — don't wait to be asked).
  Keep the log as the running source of truth for what's already built.

## When something looks wrong — report, don't fix silently
- If an API call behaves unexpectedly (wrong shape, error status, missing/odd data)
  or seems buggy on the backend side, **stop and report it to the user** — describe
  what's wrong and the likely cause. Do NOT change API/backend code or "work around" it.
- If the design reference conflicts with the current data/API, **flag the mismatch and ask**
  before deciding how to reconcile it.
- Make no fix outside the immediate frontend task without explicit permission from the user.

## Don't
- Don't touch backend/API projects from here.
- Don't hardcode task data — fetch through `TodoApiService`.
- Don't invent new accent colors; reuse the palette above.

## Done log
Completed & verified frontend work. Newest at the bottom. Append here when a task is done.

- 2026-07-12 — **Board view** (`Pages/Board.razor` + `.razor.css`, `@layout BlankLayout`).
  Header (logo + tabs List/Board/Week/Stats + date + search + Add) · stats strip
  (Hôm nay/Đang làm/Trễ hạn/Hoàn thành) · 4 horizontal groups Trễ hạn/Cần làm/Đang làm/Hoàn thành ·
  card with done-toggle, start/stop, delete, tags, priority, due label · client-side search ·
  quick-add (Enter in Cần làm) · composer modal (title/desc/dates/priority/tags/ref).
  Wired to real API via `TodoApiService` (+ `GetTagsAsync`/`GetPrioritiesAsync`). Verified rendering with live data.
  Deferred: task-detail modal, "→ today" reschedule, List/Week/Stats tabs (placeholder), streak stat (shows Done total instead).

- 2026-07-13 — **List view** (`Pages/Board.razor` `List` tab + `.bd-list-*`/`.bd-row-*` in `.razor.css`).
  Vertical list grouped by the 4 status buckets (reuses `Columns()`); empty groups hidden.
  Row = round done-toggle · title (strike+dim when done) · tags · priority · due label (mono, right) ·
  start/stop by bucket · delete. Reuses `Bucket`/`Filtered`/`DueLabel` + all actions; client-side search shared with Board.
  Also: Board quick-add input in "Cần làm" replaced by a `+ Thêm công việc` button that opens the composer
  (removed `_quick`/`QuickKey`); composer no longer defaults Start/Due dates (empty).
  Verified with 10 seeded tasks covering all 4 groups.
  Deferred: task-detail modal, "→ today" reschedule, Week/Stats tabs.
  Backend notes: `POST /api/todoitems` originally returned 201 with no body/id — **since fixed** (see 2026-07-13
  backend entry below, all POSTs now return the created object + `Location`). Duplicate "Urgent" priority rows
  also cleaned up during the data-restore.

- 2026-07-13 — **Week view** (`Pages/Board.razor` `Week` tab + `.bd-week-*` in `.razor.css`).
  7-column grid Mon→Sun (`T2`…`CN`) for the current week; each column lists tasks whose `DueDate.Date`
  falls on that day (shared `Filtered()` so search applies). Today's column accented + "hôm nay" badge.
  Compact card = done-toggle · title (strike+dim when done) · delete · meta row (tags + time if any).
  Helpers: `WeekDays()` (Monday-anchored), `DowVi()`, `WeekTime()`. Reuses `Bucket`/`DueLabel` + actions.
  Per-column vertical scroll: `.bd-week` uses `grid-template-rows: minmax(0,1fr)` so a long day scrolls
  inside its column instead of overflowing; thin scrollbar styled for `.bd-week-day` (+ `.bd-list`).
  Tasks with no due date, or due outside the current week, don't appear (by design — weekly agenda).
  Verified with seeded tasks incl. 9 stacked on today to confirm column scroll.
  Deferred: prev/next-week navigation, drag-to-reschedule, task-detail modal, Stats tab.

- 2026-07-13 — **Stats view** (`Pages/Board.razor` `Stats` tab + `.bd-stat*`/`.bd-dist*`/`.bd-tagstat*`/`.bd-ov*` in `.razor.css`).
  Read-only 2×2 grid, **only real computable data** (no fake history — API has no completion-date field):
  ① **Tiến độ hôm nay** — big % + segment strip (1 seg per task due today, green=done) + `X/Y`.
  ② **Phân bố trạng thái theo tuần** — 4 bars (Trễ hạn/Cần làm/Đang làm/Hoàn thành) counting only tasks whose
     `DueDate` is in the current week; shows the week range `Tuần dd/MM → dd/MM`. Helper `BucketedWeek()`.
  ③ **Theo tag** — one row per tag, sorted by most-used (`OrderByDescending(Total)`); each row a distinct color
     from `TagColors` (prototype palette, no new accents) — dot + name + `done/total` + colored progress bar.
  ④ **Tổng quan** — Đã hoàn thành / Trễ hạn / Tỉ lệ hoàn thành %, then an explicit **list of overdue tasks**
     (name + due, red) via `OverdueTasks()`; falls back to `OverviewNote` when none overdue.
  Helpers: `TotalCount`, `Bucketed`/`BucketedWeek`, `TodayPct`, `DonePct`, `TodayTasks`, `TagStats()`/`TagStat`,
  `OverdueTasks()`, `OverviewNote`; refactored `WeekStart()`/`WeekEnd()` out of `WeekDays()` (shared with Week tab).
  Note: `.bd-stats-*` prefix chosen so it doesn't collide with the header strip's `.bd-stats`/`.bd-stat`.
  Dropped from prototype (need per-day completion history the API lacks): "last 7 days" chart, streak.
  Deferred: time-range filter, real time-series once API exposes a completed-at date.

All four tabs (List/Board/Week/Stats) are now built. Remaining app-wide deferrals: task-detail modal, "→ today"
reschedule, prev/next-week nav, drag-to-reschedule.

- 2026-07-13 — **Edit task (click-to-edit) + reference link**.
  Clicking a task title anywhere (Board card / List row / Week card — class `bd-title-click`) opens the
  **composer in edit mode directly** (no separate view modal — an earlier read-only detail modal was built
  then removed per feedback: "nhấn vào là sửa được luôn").
  Composer is dual-mode via `_editingId` (null=create/`Thêm`, set=edit/`Lưu`); `OpenEdit(t)` pre-fills all
  drafts; `SubmitComposer` branches `CreateAsync` vs new `UpdateAsync(id,dto)` → `PUT /api/todoitems/{id}`
  (body reuses `CreateTodoItemDto`; API's `UpdateTodoItemDto` has the same shape). PUT verified live (200).
  Edit mode also shows a red **Xóa** button (`DeleteEditing()`); create mode doesn't.
  Reference field is now a `type="url"` link input; when it holds a valid http/https URL a "↗ Mở liên kết"
  anchor appears (`target=_blank rel=noopener`, opens immediately). `IsHttpUrl()` guards http/https only
  (blocks `javascript:` etc.). New CSS: `.bd-title-click`, `.bd-btn.danger`, `.bd-ref-open`.
  Deferred: adding a time field + multi-link references in the composer.

- 2026-07-13 — **Backend: POST endpoints now return the created object (with id).** User changed all three
  create paths (tags, priorities, todoitems): service `Create*Async` returns the response DTO, controller
  returns `CreatedAtAction(...)` → `201 Created` + `Location` header + body incl. `id`. For todoitems the
  service reloads via `GetTodoItemByIdAsync(newId)` so `Priority`/`Tags` navigation is populated. All three
  verified live (201 + Location + id). PUT/DELETE deliberately left as-is (client already has the id; the
  refetch-after-mutation pattern doesn't need a return body).

- 2026-07-13 — **Create + delete Tag/Priority from the composer** (`Pages/Board.razor` + `.bd-chip-*` CSS).
  Create: dashed input at the end of the Tag and Priority chip rows ("+ tag mới ⏎" / "+ mức mới ⏎").
  Enter → if the name already exists (case-insensitive) just select it (no duplicate); else
  `CreateTagAsync`/`CreatePriorityAsync` POST and **read the new id straight from the 201 response body**
  (no GET-refetch-by-name — enabled by the backend change above), append the chip, auto-select it.
  Delete: one **⋮ button per group** toggles a delete-mode (`_tagDeleteMode`/`_priDeleteMode`); in that mode
  chips turn red and a left-click deletes that item (`TagChipClick`/`PriChipClick` route to delete vs select).
  Deleting an in-use **priority** returns 500 (FK) — surfaced as an error message, nothing corrupted
  (verified live); tags are M2M so delete just unlinks. New API methods: `CreateTagAsync`/`CreatePriorityAsync`
  (return DTO), `DeleteTagAsync`/`DeletePriorityAsync`. CSS: `.bd-chip-add`, `.bd-chip-mgr(.active)`,
  `.bd-chip.del`, `.bd-del-hint`.
  Note (process hygiene): editing DB data via PowerShell, ALWAYS parse with explicit `ConvertFrom-Json`
  and delete by exact numeric id — never filter by name/wildcard. A `Where-Object -like/-eq` on
  `Invoke-RestMethod` output matched every row (its array-valued props make `-eq` a filter, not a bool)
  and wiped all data once; it was fully restored afterward.
