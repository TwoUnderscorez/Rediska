- Fix todos
- tests for commands
  - auxiliary
  - Connection
  - Geo
  - Hashes
  - HyperLogLog
  - Keys
  - Lists
  - Pub/Sub
  - Scripting
  - Server
  - Sets
  - SortedSets
  - Transactions

- implement pipelining connection
- implement connections pool
  - with implicit owning
  - with explicit owning for blocking or connection mutating commands
- implement Pub/Sub
- implement Stream commands