FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install MongoDB CLI (optional, for interacting with DB)
RUN apt-get update && \
    apt-get install -y gnupg curl && \
    curl -fsSL https://pgp.mongodb.com/server-6.0.asc | gpg --dearmor -o /usr/share/keyrings/mongodb-server-6.0.gpg && \
    echo "deb [ signed-by=/usr/share/keyrings/mongodb-server-6.0.gpg ] https://repo.mongodb.org/apt/debian bullseye/mongodb-org/6.0 main" | tee /etc/apt/sources.list.d/mongodb-org-6.0.list && \
    apt-get update && apt-get install -y mongodb-org-shell

# Optional: Install Docker CLI if needed
RUN apt-get install -y docker.io

# Start MongoDB via Docker
CMD ["bash"]
