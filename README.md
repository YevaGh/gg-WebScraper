# Website Scraping and Data Visualization Project

This project is designed to scrape information from websites, process it, store it in a PostgreSQL database, and visualize it using an Angular web application.

## Overview

The project consists of the following components:

1. **Website Scraping Executables**:
   - `exe-RateAmTerminal`: This executable scrapes data from [rate.am](https://rate.am/) using RateAmLib, RateAmData.
   - `exe-RateAmApi`: This executable provides Api for HTTP requests.

2. **Libraries**:
   - `RateAmLib`: This library provides utilities for processing scraped data, Services.
   - `RateAmData`: This library handles database interactions, Entities, Repositories.

3. **Database**:
   - PostgreSQL is used as the database to store the scraped data.

4. **Angular Web Application**:
   - `RateAmWeb`: This Angular project visualizes the scraped data in various charts and tables.

## Usage

### Setting Up the Environment

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/your-project.git
   ```

2. **Install Dependencies**:
   - Navigate to each component's directory and follow the instructions in their README files to install dependencies.

### Running the Executables

1. **Run the Website Scraping Executables**:
   - Follow the instructions provided in each component's README file to run the executables.

### Setting Up the Database

1. **Create the PostgreSQL Database**:
   - Follow the instructions provided in the `database-setup.md` file in the `database` directory to set up the PostgreSQL database.

2. **Import the Schema**:
   - If necessary, import the database schema using the instructions provided in the `database-schema.sql` file in the `database` directory.

### Running the Angular Web Application

1. **Start the Angular Development Server**:
   ```bash
   cd angular-project
   ng serve
   ```

2. **Access the Application**:
   - Open your web browser and navigate to `http://localhost:4200`.

## Contributing

If you would like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit them (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a new Pull Request.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgements

- Special thanks to [acknowledged individual/organization] for their contributions to this project.


##docker-compose file 

```bash
version: '3.9'
services:
  postgres:
    image: postgres:16-alpine
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: password
      POSTGRES_DB: fromdocker

  myredis:
    image: redis:7-alpine
    ports:
      - "6379:6379"

  selenium:
    image: selenium/standalone-chrome
    ports:
      - "4444:4444"
    restart: always

  myterminal:
    image: yevka/rateam-myterminal:latest
    environment:
      - ConnectionStrings__RateDb=Host=postgres;Port=5432;Database=fromdocker;Username=postgres;Password=password
      - ConnectionStrings__Redis=Host=myredis;Port=6379
    depends_on:
      - postgres
      - myredis
      - selenium

  myapi:
    image: yevka/rateam-myapi:latest
    ports:
      - "5002:5002"
    depends_on:
      - myterminal

  myweb:
    image: yevka/rateam-myweb:latest
    ports:
      - "4200:4200"
    environment:
      - API_HOST=myapi
      - API_PORT=5002
    depends_on:
      - myapi
```
