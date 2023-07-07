const sql = require('mssql');

const config = {
  server: 'LAPTOP-B1MP3VOA', // SQL Server instance
  database: 'testdb', // Your database name
  options: {
    trustedConnection: true // Use Windows authentication
  }
};

sql.connect(config)
  .then(pool => {
    console.log('Connected to SQL Server');

    // Use the connection to query the database
    return pool.request().query('SELECT * FROM tbl_students');
  })
  .then(result => {
    console.log(result.recordset);
  })
  .catch(err => {
    console.log('Error: ', err);
  });
