# ASP.NET Core Advanced Concepts Demo

This repository contains demos and examples of advanced concepts in ASP.NET Core Web API, including authentication, payment services, caching, logging, messaging, and more. It is designed to help developers learn and implement these features in real-world applications.

---

## 🔑 Authentication and Authorization
- JWT-based authentication
- Role-based and policy-based authorization
- Securing API endpoints

---

## 🛡️ JWT Token
- Generate and validate JSON Web Tokens (JWT)
- Implement refresh tokens
- Secure API with Bearer token authentication

---

## 📜 Logger
- Structured logging using `ILogger`
- Log levels: Trace, Debug, Information, Warning, Error, Critical
- Logging to Console, File, or external systems (Serilog, NLog)

---

## ⚙️ Middlewares
- Custom middleware examples
- Exception handling middleware
- Request logging middleware
- Pipeline configuration in ASP.NET Core

---

## 📧 Email Service
- Sending emails using SMTP / MailKit
- HTML emails and attachments
- Async email sending

---

## 📌 API Versioning
- URL versioning (e.g., `/api/v1/`)
- Header-based versioning
- Deprecation handling

---

## 💳 Payment Service
- Stripe, PayPal, Paymob, FawryPay, Etisalat Cash integrations
- Test & sandbox environments
- Webhooks and payment confirmation handling

---

## 🔔 Firebase Push Notification
- FCM setup and configuration
- Send notifications to Android, iOS, and Web
- Topic-based and device-based messaging

---

## 🔍 ElasticSearch & Kibana
- Create indexes and store/search documents
- Query DSL examples
- Kibana dashboards for visualization

---

## 🗄️ Redis & Caching
- In-memory caching
- Distributed caching with Redis
- Cache expiration strategies (Sliding, Absolute)
- Cache patterns: Cache-aside, Read-through

---

## 🐇 RabbitMQ
- Producer & Consumer examples
- Messaging patterns: Work Queues, Publish/Subscribe, Routing
- Durable queues, acknowledgements, prefetch

---

## 🚦 Request Limiting (Rate Limiting)
- Fixed window, sliding window, and token bucket
- Limit API requests per client
- Handling `429 Too Many Requests` responses

---

## 📂 Getting Started
1. Clone the repository:
```bash
git clone https://github.com/yourusername/aspnetcore-advanced-demo.git
