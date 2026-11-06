#!/usr/bin/env node
// Ger varje worktree en egen port och pekar klienten mot rätt API.
//   node scripts/worktree/worktree.mjs ensure        skapar .worktree/env.json och wwwroot/appsettings.Development.json
//   node scripts/worktree/worktree.mjs drop          tar bort filerna
//   node scripts/worktree/worktree.mjs exec <cmd>    kör ett kommando med worktreens inställningar
//
// API-adressen tas från MINTAKT_API_URL och är annars http://localhost:5137/.
import { spawnSync } from 'node:child_process';
import { createHash } from 'node:crypto';
import { existsSync, mkdirSync, readFileSync, rmSync, writeFileSync } from 'node:fs';
import { join, resolve } from 'node:path';

const worktree = process.env.ORCA_WORKTREE_PATH || process.cwd();
const stateDir = join(worktree, '.worktree');
const settings = join(worktree, 'wwwroot', 'appsettings.Development.json');
const [command, ...args] = process.argv.slice(2);

if (command === 'ensure') {
  const hash = createHash('sha1').update(resolve(worktree).toLowerCase()).digest('hex').slice(0, 8);
  const port = 5900 + (parseInt(hash, 16) % 600);
  const apiBaseUrl = process.env.MINTAKT_API_URL || 'http://localhost:5137/';
  mkdirSync(stateDir, { recursive: true });
  writeFileSync(join(stateDir, 'env.json'), JSON.stringify({ env: { ASPNETCORE_ENVIRONMENT: 'Development', ASPNETCORE_URLS: `http://localhost:${port}` } }, null, 2));
  writeFileSync(settings, JSON.stringify({ ApiBaseUrl: apiBaseUrl }, null, 2));
  console.log(`Klart. Klientadress: http://localhost:${port}, API: ${apiBaseUrl}`);
} else if (command === 'drop') {
  rmSync(settings, { force: true });
  rmSync(stateDir, { recursive: true, force: true });
} else if (command === 'exec' && args.length) {
  const file = join(stateDir, 'env.json');
  if (!existsSync(file)) {
    console.error('Kör "ensure" först.');
    process.exit(1);
  }
  const { env } = JSON.parse(readFileSync(file, 'utf8'));
  const result = spawnSync(args[0], args.slice(1), { stdio: 'inherit', env: { ...process.env, ...env }, shell: process.platform === 'win32' });
  process.exit(result.status ?? 1);
} else {
  console.error('Använd: ensure, drop eller exec <kommando>');
  process.exit(1);
}
